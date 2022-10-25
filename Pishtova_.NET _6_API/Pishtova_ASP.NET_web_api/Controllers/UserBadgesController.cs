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

        public UserBadgesController(
            IBadgeService badgeService,
            IUsersBadgesService usersBadgesService
            )
        {
            this.badgeService = badgeService;
            this.usersBadgesService = usersBadgesService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] UserBadgeInputModel inputModel)
        {
            try
            {
                var badge = await this.badgeService.GetByCodeAsync(inputModel.BadgeCode);
                if (badge == null) throw new System.Exception("Uncorrect badge code in input model!");

                //TODO Mapper!!
                var model = new UserBadge
                {
                    UserId = inputModel.UserId,
                    TestId = inputModel.TestId,
                    BadgeId = badge.Id,
                };
                await this.usersBadgesService.CreatAsync(model);
                return StatusCode(201);
            }
            catch (System.Exception e)
            {
                return StatusCode(400, new ErrorResult { Message = e.Message });
            }

        }

        [HttpGet]
        [Route("[action]/{userId}")]
        public async Task<ICollection<BadgeCountModel>> GetAll(string userId)
        {
            //TODO Validate param
            var badges = await this.usersBadgesService.GetAllByUserAsync(userId);
            var result = CreateBadgeCountModelCollection(badges);
            return result;
        }

        [HttpGet]
        [Route("[action]/{testId}")]
        public async Task<ICollection<BadgeCountModel>> GetTestAll(int testId)
        {
            //TODO Validate param
            var badges = await this.usersBadgesService.GetAllByTestAsync(testId);
            var result = CreateBadgeCountModelCollection(badges);
            return result;
        }


        private static ICollection<BadgeCountModel> CreateBadgeCountModelCollection(ICollection<UserBadge> badges)
        {
            var result = new List<BadgeCountModel>();
            foreach (var item in badges)
            {
                var badgeModel = result.FirstOrDefault(x => x.Code == item.Badge.Code);
                if (badgeModel == null)
                {
                    badgeModel = new BadgeCountModel { Code = item.Badge.Code, Count = 0 };
                    result.Add(badgeModel);
                }
                badgeModel.Count += 1;
            }
            return result;
        }
    }
}
