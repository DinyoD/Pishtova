﻿namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Threading.Tasks;

    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userservice)
        {
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
        [Route(nameof(UpdateAvatar))]
        public async Task<IActionResult> UpdateAvatar([FromBody]string pictureUrl)
        {

            if (string.IsNullOrEmpty(pictureUrl))
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect pictureUrl" });
            }

            try
            {
                var userId = this.userService.GetUserId(User);
                await this.userService.UpdateUserAvatar(userId, pictureUrl);
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(400, new ErrorResult { Message = "Uncorrect request" });
            }
        }

    }
}