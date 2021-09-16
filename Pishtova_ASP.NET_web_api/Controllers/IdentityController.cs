﻿namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.Identity;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationSettings applicationSettings;

        public IdentityController(
            UserManager<User> userManager,
            ApplicationSettings applicationSettings)
        {
            this.userManager = userManager;
            this.applicationSettings = applicationSettings;
        }

        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PictureUrl = model.PictureUrl,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [Route(nameof(Login))]
        public async Task<ActionResult<string>> Login(LoginUserModel model)
        {

            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(this.applicationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, null)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
