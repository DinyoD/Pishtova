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
        [Route(nameof(GetProfile))]
        public ActionResult<UserModel> GetProfile()
        {
            try
            {
                var userId = this.userService.GetUserId(User);
                return this.userService.GetProfileInfo(userId);
            }
            catch
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect request" });
            }
        }

        [HttpPut]
        [Route(nameof(UpdatePictureUrl))]
        public async Task<IActionResult> UpdatePictureUrl([FromBody] UserUpdatePictureUrlModel model)
        {

            if (string.IsNullOrEmpty(model.PictureUrl))
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect pictureUrl" });
            }

            try
            {
                var userId = this.userService.GetUserId(User);
                await this.userService.UpdateUserAvatar(userId, model.PictureUrl);
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect request" });
            }
        }

        [HttpPut]
        [Route(nameof(UpdateUserInfo))]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserUpdateInfoModel model)
        {

            if (model == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect model" });
            }

            try
            {
                var userId = this.userService.GetUserId(User);
                var user = await this.userService.UpdateUserInfo(userId, model);
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect request" });
            }
        }

        [HttpPut]
        [Route(nameof(UpdateUserEmail))]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UserChangeEmailModel model)
        {
            if (model == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect model" });
            }

            try
            {
                var userId = this.userService.GetUserId(User);
                var user = await this.userService.UpdateUserEmail(userId, model);
                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                await this.userService.SendEmailConfirmationTokenAsync(model.ClientURI, user.Email, token);
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect request" });
            }
        }

    }
}
