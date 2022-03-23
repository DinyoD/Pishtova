namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.UserBadge;

    public class UserBadgesController : ApiController
    {
        private readonly IBadgeService badgeService;
        private readonly IUsersBadgesService usersBadgesService;
        private readonly IUserService userService;

        public UserBadgesController(
            IBadgeService badgeService,
            IUsersBadgesService usersBadgesService,
            IUserService userService)
        {
            this.badgeService = badgeService;
            this.usersBadgesService = usersBadgesService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Save ([FromBody] UserBadgeDTO data)
        {
            try
            {
                var userId = this.userService.GetUserId(User);
                var badgeId = await this.badgeService.GetBadgeIdByCodeAsync(data.BadgeCode);
                var model = new UserBadgeModel
                {
                    UserId = userId,
                    BadgeId = badgeId,
                    TestId = data.TestId
                };
                await this.usersBadgesService.CreateUserBadgeAsync(model);
                return StatusCode(201);
            }
            catch (System.Exception)
            {
                return StatusCode(400, new ErrorResult { Message = "Server error" });
            }

        }

        [HttpGet]
        [Route("{id}/[action]")]
        public async Task<UserBadgesCountDTO> All(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = this.userService.GetUserId(User);
            }

            var badges = await this.usersBadgesService.GetUserAllBadgesAsync(id);
            var result = CreateUserBadgesModel(badges);
            return result;
        }

        private static UserBadgesCountDTO CreateUserBadgesModel(ICollection<UserBadgeWithCodeModel> badges)
        {
            var result = new UserBadgesCountDTO();
            foreach (var item in badges)
            {
                var badgeModel = result.Badges.FirstOrDefault(x => x.Code == item.Code);
                if (badgeModel == null)
                {
                    badgeModel = new BadgeCountModel { Code = item.Code, Count = 0 };
                    result.Badges.Add(badgeModel);
                }
                badgeModel.Count += 1;
            }
            return result;
        }
    }
}
