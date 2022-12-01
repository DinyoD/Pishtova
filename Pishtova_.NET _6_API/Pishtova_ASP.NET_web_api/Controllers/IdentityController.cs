namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Pishtova.Data.Common.Utilities;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Identity;

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
            if (applicationSettings is null) throw new ArgumentNullException(nameof(applicationSettings));

            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            this.applicationSettings = applicationSettings.Value;
        }

        // TODO Implement Pishtova status-code
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody]UserRegisterModel model)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(model)) return this.Error(operationResult);

            //TODO Validate data with OperationErrorValidation

            // TODO Mapper
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
                var errors = result.Errors.Select(x => new Error { Message = x.Description }).ToList();
                operationResult.AddError(errors[1]);
                return this.Error(operationResult);               
            }

            try
            {
                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                var sendMailResult = await this.userService.SendEmailConfirmationTokenAsync(model.ClientURI, model.Email, token);
                if (!sendMailResult.IsSuccessful) 
                {
                    this.DeleteUserByEmail(user.Email);
                    return this.Error(sendMailResult);
                }
                return StatusCode(201);
            }
            catch (Exception e)
            {
                this.DeleteUserByEmail(user.Email);

                operationResult.AddException(e);
                return this.Error(operationResult);
            }

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(model)) return this.Error(operationResult);

            //TODO Validate data with OperationErrorValidation

            // TODO Mapper
            var user = await this.userManager.Users.Include(x => x.Subsriber).FirstOrDefaultAsync(x => x.Email == model.Email);
            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !passwordValid) operationResult.AddError(new Error { Message = "Sorry, your username and/or password do not match" });
            if (!operationResult.IsSuccessful) return this.Error(operationResult);

            if (!await this.userManager.IsEmailConfirmedAsync(user)) operationResult.AddError(new Error { Message = "Sorry, your email is not confirmed" });
            if (!operationResult.IsSuccessful) return this.Error(operationResult);

            try
            {
                var subscription = user.Subsriber != null ? await this.subscriptionService.GetByCustomerIdAsync(user.Subsriber.CustomerId): null;
                DateTime expDate = DateTime.Now.AddDays(7);
                var isSubscriber = subscription != null && subscription.Status == "active";

                string generatedToken = this.GenerateToken(user, expDate, isSubscriber);
                return this.Ok(new { token = generatedToken });
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
                return this.Error(operationResult);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(email)) return this.Error(operationResult);
            if (!operationResult.ValidateNotNull(token)) return this.Error(operationResult);

            var user = await this.userManager.FindByEmailAsync(email);
            if (user == null) operationResult.AddError(new Error { Message = "Sorry, your username and/or password do not match" });
 
            var confirmResult = await this.userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded) operationResult.AddError(new Error { Message = "The form is not fulfilled correctly!" });

            if (!operationResult.IsSuccessful) return this.Error(operationResult);

            return this.Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(model)) return this.Error(operationResult);

            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null) operationResult.AddError( new Error { Message = "Your email is not correct!" });
            if (!operationResult.IsSuccessful) return this.Error(operationResult);

            try
            {
                var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
                var sendMailResult = await this.userService.SendResetPaswordTokenAsync(model.ClientURI, model.Email, token);
                if (!sendMailResult.IsSuccessful) return this.Error(sendMailResult);
                return this.Ok();
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
                return this.Error(operationResult);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(model)) return this.Error(operationResult);

            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null) operationResult.AddError(new Error { Message = "Your email is not correct!" });
            if (!operationResult.IsSuccessful) return this.Error(operationResult);

            var resetPassResult = await this.userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(x => new Error { Message = x.Description }).ToList();
                operationResult.AddError(errors[1]);
                return this.Error(operationResult);
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

        private async void DeleteUserByEmail(string email)
        {
            var createdUser = await this.userManager.FindByEmailAsync(email);
            await this.userManager.DeleteAsync(createdUser);
        }
    }
}
