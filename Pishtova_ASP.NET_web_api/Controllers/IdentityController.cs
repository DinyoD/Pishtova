namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using Pishtova.Data.Model;
    using Pishtova.Services.Messaging;
    using Pishtova_ASP.NET_web_api.Model.Identity;
    using Pishtova_ASP.NET_web_api.Model.Result;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;
        private readonly ApplicationSettings applicationSettings;

        public IdentityController(
            UserManager<User> userManager,
            IOptions<ApplicationSettings> applicationSettings,
            IEmailSender emailSender)          
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.applicationSettings = applicationSettings.Value;
        }

        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            var registerResult = new RegisterResult();
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
                Grade = model.Grade,
                SchoolId = model.SchoolId
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                registerResult.ErrorsMessages = result.Errors.Select(x => x.Description).ToList();
                return BadRequest(registerResult);
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string>
                            {
                                {"token", token },
                                {"email", user.Email }
                            };
            var callback = QueryHelpers.AddQueryString(model.ClientURI, param);
            var message = new Message(new string[] { user.Email }, "Email Confirmation token", callback, null);
            await this.emailSender.SendEmailAsync(message);

            return StatusCode(201, registerResult);
        }

        [Route(nameof(Login))]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            var loginResult = new LoginResult();
            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                loginResult.ErrorsMessages.Add("Sorry, your username and/or password do not match");
                return Unauthorized(loginResult);
            }

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                loginResult.ErrorsMessages.Add("Sorry, your email is not confirmedh");
                return Unauthorized(loginResult);
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                loginResult.ErrorsMessages.Add("Sorry, your username and/or password do not match");
                return Unauthorized(loginResult);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            loginResult.Token= tokenHandler.WriteToken(token);

            return Ok(loginResult);
        }

        [HttpGet(nameof(EmailConfirmation))]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");

            var confirmResult = await userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");

            return Ok();
        }
    }
}
