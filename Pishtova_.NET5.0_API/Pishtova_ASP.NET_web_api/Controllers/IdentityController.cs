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
    using Pishtova.Services.Data;
    using Pishtova.Services.Messaging;
    using Pishtova_ASP.NET_web_api.Model.Identity;
    using Pishtova_ASP.NET_web_api.Model.Results;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IPishtovaSubscriptionService subscriptionService;
        private readonly ApplicationSettings applicationSettings;

        public IdentityController(
            UserManager<User> userManager,
            IOptions<ApplicationSettings> applicationSettings,
            IUserService userService,
            IPishtovaSubscriptionService subscriptionService)          
        {
            this.userManager = userManager;
            this.userService = userService;
            this.subscriptionService = subscriptionService;
            this.applicationSettings = applicationSettings.Value;
        }

        // TODO Implement Pishtova status-code
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO data)
        {
            if (data == null)
            {
                return StatusCode(400, new ErrorResult { Message = "The form is not fulfilled correctly!" });
            }
            var user = new User
            {
                Name = data.Name,
                Email = data.Email,
                UserName = data.Email,
                Grade = data.Grade,
                SchoolId = data.SchoolId
            };

            var result = await this.userManager.CreateAsync(user, data.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return StatusCode(400, new ErrorResult { Message = errors[1] });
            }
            var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            await this.userService.SendEmailConfirmationTokenAsync(data.ClientURI, data.Email, token);

            return StatusCode(201);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO data)
        {
            var user = await this.userManager.FindByEmailAsync(data.Email);

            if (user == null)
            {
                return StatusCode(401, new ErrorResult { Message = "Sorry, your username and/or password do not match" });
            }

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                return StatusCode(401, new ErrorResult { Message = "Sorry, your email is not confirmed" });
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, data.Password);

            if (!passwordValid)
            {
                return StatusCode(401, new ErrorResult { Message = "Sorry, your username and / or password do not match" });
            }

            var subscription = await this.subscriptionService.GetByCustomerIdAsync(user.CustomerId);
            DateTime expDate = DateTime.Now.AddDays(7);
            var isSubscriber = subscription != null && subscription.CurrentPeriodEnd > DateTime.Now;

            string token = this.GenerateToken(user, expDate, isSubscriber);
            return StatusCode(200, new LoginResult { Token = token });


        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Invalid Email Confirmation Request" });
            }

            var confirmResult = await userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
            {
                return StatusCode(400, new ErrorResult { Message = "The form is not fulfilled correctly!" });
            }

            return StatusCode(200);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO data)
        {
            var user = await userManager.FindByEmailAsync(data.Email);
            if (user == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Your email is not correct!" });
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            await this.userService.SendResetPaswordTokenAsync(data.ClientURI, data.Email, token);

            return StatusCode(200);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO data)
        {
            var user = await userManager.FindByEmailAsync(data.Email);

            if (user == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Your email is not correct!" });
            }

            var resetPassResult = await userManager.ResetPasswordAsync(user, data.Token, data.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description).ToList();

                return StatusCode(400, new ErrorResult { Message = errors[0] });
            }

            return Ok();
        }

        private string GenerateToken(User user, DateTime expDate, bool isSubscriber)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);
            var avatarUrl = user.PictureUrl ?? string.Empty;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("userId", user.Id),
                    new Claim("isSubscriber", isSubscriber.ToString()),
                    new Claim("avatarUrl", avatarUrl)
                }),
                Expires = expDate,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
