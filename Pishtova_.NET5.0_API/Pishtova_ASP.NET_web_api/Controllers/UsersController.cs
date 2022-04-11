namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Threading.Tasks;

    public class UsersController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public UsersController(UserManager<User> userManager, IUserService userservice)
        {
            this.userManager = userManager;
            this.userService = userservice;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<UserProfileDTO> Profile()
        {
            var userId =await this.userService.GetUserIdAsync(User);
            return await this.userService.GetProfileInfoAsync(userId);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<UserInfoDTO> Info(string id)
        {
            return await this.userService.GetUserInfoAsync(id);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdatePictureUrl([FromBody] UserPictureDTO data)
        {

            if (string.IsNullOrEmpty(data.PictureUrl))
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect pictureUrl" });
            }

            try
            {
                var userId = await this.userService.GetUserIdAsync(User);
                await this.userService.UpdateUserAvatar(userId, data.PictureUrl);
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect request" });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfoToUpdateDTO data)
        {

            if (data == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect model" });
            }

            try
            {
                var userId = await this.userService.GetUserIdAsync(User);
                await this.userService.UpdateUserInfo(userId, data);
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect request" });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UserChangeEmailDTO data)
        {
            if (data == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect model" });
            }

            try
            {
                var userId = await this.userService.GetUserIdAsync(User);
                var user = await this.userService.UpdateUserEmail(userId, data);
                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                await this.userService.SendEmailConfirmationTokenAsync(data.ClientURI, user.Email, token);
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect request" });
            }
        }

    }
}
