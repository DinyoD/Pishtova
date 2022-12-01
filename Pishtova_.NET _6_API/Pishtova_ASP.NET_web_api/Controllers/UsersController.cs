namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.User;
    using Pishtova_ASP.NET_web_api.Model.School;

    public class UsersController : ApiController
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UsersController(
            IUserService userservice,
            UserManager<User> userManager)
        {
            this.userService = userservice ?? throw new System.ArgumentNullException(nameof(userservice));
            this.userManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]string id)
        {
            var result = await this.userService.GetByIdAsync(id);
            if (!result.IsSuccessful) return this.Error(result);

            var user = result.Data;
            if (user == null) return this.NotFound();

            var profileModel = this.ToUserProfileModel(user);

            return this.Ok(profileModel);
        }

        [HttpGet]
        [Route("{id}/[action]")]
        public async Task<IActionResult> Info([FromRoute]string id)
        {
            var result = await this.userService.GetByIdAsync(id);
            if (!result.IsSuccessful) return this.Error(result);

            var user = result.Data;
            if (user == null) return this.NotFound();

            var infoModel = this.ToUserInfoModel(user);

            return this.Ok(infoModel);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdatePictureUrl([FromRoute] UserToUpdatePictireUrlModel model)
        {
            var result = await this.userService.UpdateUserAvatar(model);
            if (!result.IsSuccessful) return this.Error(result);

            return this.Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateInfo([FromBody] UserToUpdateModel data)
        {
            var result = await this.userService.UpdateUserInfo(data);
            if (!result.IsSuccessful) return this.Error(result);

            return this.Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateEmail([FromBody] UserToUpdateEmailModel data)
        {
            var result = await this.userService.UpdateUserEmail(data);
            if (!result.IsSuccessful) return this.Error(result);

            var user = result.Data;
            if (user == null) return this.NotFound();
            try
            {
                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                var sendMailResult = await this.userService.SendEmailConfirmationTokenAsync(data.ClientURI, user.Email, token);
                if (!sendMailResult.IsSuccessful) return this.Error(sendMailResult);
            }
            catch (Exception e)
            {
                var operationResult = new OperationResult();
                operationResult.AddException(e);
                return this.Error(operationResult);
            }

            return this.Ok();
        }

        private UserProfileModel ToUserProfileModel(User user)
        {
            return new UserProfileModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Grade = user.Grade,
                PictureUrl = user.PictureUrl,
                TownName = user.School.Town.Name,
                School = new SchoolBasicModel { Id = user.SchoolId, Name = user.School.Name }
            };
        }

        private UserInfoModel ToUserInfoModel(User user)
        {
            return new UserInfoModel
            {
                Name = user.Name,
                Grade = user.Grade,
                SchoolName = user.School.Name,
                TownName = user.School.Town.Name,
                PictureUrl = user.PictureUrl,
            };
        }

    }
}
