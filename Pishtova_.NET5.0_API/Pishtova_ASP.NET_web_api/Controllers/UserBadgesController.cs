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
        public async Task<IActionResult> Save ([FromBody] int code, int testId)
        {
            if (code == 0)
            {
                return StatusCode(400, new ErrorResult { Message = "The request body is empty" });
            }
            try
            {
                var userId = this.userService.GetUserId(User);
                var badge = await this.badgeService.GetBadgeByCodeAsync(code);
                var model = new UserBadgeModel
                {
                    UserId = userId,
                    BadgeId = badge.Id,
                    TestId = testId
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
        [Route("[action]/{id}")]
        public async Task<UserBadgesCountModel> All([FromQuery] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = this.userService.GetUserId(User);
            }

            var badges = await this.usersBadgesService.GetUserAllBadgesAsync(id);
            return CreateUserBadgesModel(badges);                                               
        }

        private static UserBadgesCountModel CreateUserBadgesModel(ICollection<UserBadge> badges)
        {
            var result = new UserBadgesCountModel();
            foreach (var item in badges)
            {
                var badgeModel = result.Badges.FirstOrDefault(x => x.Code == item.Badge.Code);
                if (badgeModel == null)
                {
                    badgeModel = new BadgeCountModel { Code = item.Badge.Code, Count = 0 };
                    result.Badges.Add(badgeModel);
                }
                badgeModel.Count += 1;
            }
            return result;
        }
    }
}
