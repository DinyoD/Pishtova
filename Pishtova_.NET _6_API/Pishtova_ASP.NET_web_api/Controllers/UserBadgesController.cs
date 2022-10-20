namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.User;
    using Pishtova_ASP.NET_web_api.Model.Results;
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
        public async Task<IActionResult> Create ([FromBody] UserBadgeInputModel inputModel)
        {
            try
            {
                var badgeId = await this.badgeService.GetBadgeIdByCodeAsync(inputModel.BadgeCode);

                //TODO Mapper!!
                var model = new UserBadge
                {
                    UserId = inputModel.UserId,
                    TestId = inputModel.TestId,
                    BadgeId = badgeId,
                };
                await this.usersBadgesService.CreatAsync(model);
                return StatusCode(201);
            }
            catch (System.Exception)
            {
                return StatusCode(400, new ErrorResult { Message = "Server error" });
            }

        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<UserBadgesCountDTO> GetAll(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = await this.userService.GetUserIdAsync(User);
            }

            var badges = await this.usersBadgesService.GetUserAllBadgesAsync(id);
            var result = CreateUserBadgesModel(badges);
            return result;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<UserBadgesCountDTO> GetAll()
        {
            var  userId = await this.userService.GetUserIdAsync(User);

            var badges = await this.usersBadgesService.GetUserAllBadgesAsync(userId);
            var result = CreateUserBadgesModel(badges);
            return result;
        }

        private static UserBadgesCountDTO CreateUserBadgesModel(ICollection<UserBadge> badges)
        {
            var result = new UserBadgesCountDTO();
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
