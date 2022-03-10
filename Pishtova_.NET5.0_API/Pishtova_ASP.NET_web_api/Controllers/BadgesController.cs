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

    public class BadgesController : ApiController
    {
        private readonly IBadgeService badgeService;
        private readonly IUsersBadgesService usersBadgesService;
        private readonly IUserService userService;

        public BadgesController(
            IBadgeService badgeService,
            IUsersBadgesService usersBadgesService,
            IUserService userService)
        {
            this.badgeService = badgeService;
            this.usersBadgesService = usersBadgesService;
            this.userService = userService;
        }

        [HttpPost]
        [Route(nameof(Save))]
        public async Task<IActionResult> Save([FromBody] int badgeCode)
        {
            if (badgeCode == 0)
            {
                return StatusCode(400, new ErrorResult { Message = "The request body is empty" });
            }
            try
            {
                var userId = this.userService.GetUserId(User);
                var badge = await this.badgeService.GetBadgeByCodeAsync(badgeCode);
                await this.usersBadgesService.CreateUserBadgeAsync(userId, badge.Id);
                return StatusCode(201);
            }
            catch (System.Exception)
            {
                return StatusCode(400, new ErrorResult { Message = "Server error" });
            }

        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<UserBadgesModel> UserAll([FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = this.userService.GetUserId(User);
            }

            var badges = await this.usersBadgesService.GetUserAllBadgesAsync(userId);
            return CreateUserBadgesModel(badges);                                               
        }

        private static UserBadgesModel CreateUserBadgesModel(ICollection<UserBadge> badges)
        {
            var result = new UserBadgesModel();
            foreach (var item in badges)
            {
                var badgeModel = result.Badges.FirstOrDefault(x => x.Code == item.Badge.Code);
                if (badgeModel == null)
                {
                    badgeModel = new BadgeModel { Code = item.Badge.Code, Count = 0 };
                    result.Badges.Add(badgeModel);
                }
                badgeModel.Count += 1;
            }
            return result;
        }
    }
}
