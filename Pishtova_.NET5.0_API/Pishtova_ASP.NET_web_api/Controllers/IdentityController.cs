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

        // TODO Implement Pishtova status-code
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
            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return StatusCode(401, new ErrorResult { Message = "Sorry, your username and/or password do not match" });
            }

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                return StatusCode(401, new ErrorResult { Message = "Sorry, your email is not confirmed" });
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
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

            return StatusCode(200, new LoginResult { Token = tokenHandler.WriteToken(token) });
        }

        [HttpGet(nameof(EmailConfirmation))]
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

        [HttpPost(nameof(ForgotPassword))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Your email is not correct!" });
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", model.Email }
            };

            var callback = QueryHelpers.AddQueryString(model.ClientURI, param);

            var message = new Message(new string[] { model.Email }, "Reset password token", callback, null);
            await emailSender.SendEmailAsync(message);

            return StatusCode(200);
        }

        [HttpPost(nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Your email is not correct!" });
            }

            var resetPassResult = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description).ToList();

                return StatusCode(400, new ErrorResult { Message = errors[0] });
            }

            return Ok();
        }
    }
}