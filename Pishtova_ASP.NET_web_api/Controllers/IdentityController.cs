﻿namespace Pishtova_ASP.NET_web_api.Controllers
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
    using Pishtova_ASP.NET_web_api.Model.OperationResult;
    using Pishtova_ASP.NET_web_api.Model.Results;

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
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            if (model == null)
            {
                return StatusCode(400, new ErrorResult { Message = "The form is not fulfilled correctly!" });
            }
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
                var errors = result.Errors.Select(x => x.Description).ToList();
                return StatusCode(400, new ErrorResult { Message = errors[0] });
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

            return StatusCode(201);
        }

        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            //var loginOperationResult = new OperationResult<ILoginResult>();
            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ///loginOperationResult.AddErrorMessage("Sorry, your username and/or password do not match");
                return StatusCode(401, new ErrorResult { Message = "Sorry, your username and/or password do not match" });
            }

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                //loginOperationResult.AddErrorMessage("Sorry, your email is not confirmed");
                return StatusCode(401, new ErrorResult { Message = "Sorry, your email is not confirmed" });
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                ///loginOperationResult.AddErrorMessage("Sorry, your username and/or password do not match");
                return StatusCode(401, new ErrorResult { Message = "Sorry, your username and / or password do not match" });
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
            //loginOperationResult.SetData(new LoginResult{Token = tokenHandler.WriteToken(token)});

            return StatusCode(200, new LoginResult { Token = tokenHandler.WriteToken(token) });
        }

        [HttpGet(nameof(EmailConfirmation))]
        public async Task<ActionResult<IVoidOperationResult>> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var emailConfirmationResult = new VoidOperationResult();
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                emailConfirmationResult.AddErrorMessage("Invalid Email Confirmation Request");
                return BadRequest(emailConfirmationResult);
            }

            var confirmResult = await userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
            {
                emailConfirmationResult.AddErrorMessage("Invalid Email Confirmation Request");
                return BadRequest(emailConfirmationResult);
            }

            return StatusCode(200, emailConfirmationResult);
        }
    }
}
