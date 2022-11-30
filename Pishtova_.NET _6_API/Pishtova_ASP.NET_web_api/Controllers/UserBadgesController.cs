namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.User;
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
            this.badgeService = badgeService ?? throw new ArgumentNullException(nameof(badgeService));
            this.usersBadgesService = usersBadgesService ?? throw new ArgumentNullException(nameof(usersBadgesService));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await this.usersBadgesService.GetByIdAsync(id);
            if (!result.IsSuccessful) return this.Error(result);

            var userBadge = result.Data;
            if (userBadge is null) return this.NotFound();

            //TODO implement and return ViewModel
            return this.Ok(userBadge);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserBadgeInputModel inputModel)
        {
            var result = await this.badgeService.GetByCodeAsync(inputModel.BadgeCode);
            if (!result.IsSuccessful) return this.Error(result);

            //TODO Mapper!!
            var userBadge = new UserBadge
            {
                UserId = inputModel.UserId,
                TestId = inputModel.TestId,
                BadgeId = result.Data.Id,
            };

            var createResult = await this.usersBadgesService.CreateAsync(userBadge);
            if (!createResult.IsSuccessful) return this.Error(createResult);

            return this.CreatedAtAction("GetById", new { Id = createResult.Data}, new UserBadgeBasicVewModel { UserBadgeId = createResult.Data});
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<IActionResult> GetUserAll([FromRoute]string userId)
        {
            var result = await this.usersBadgesService.GetAllByUserAsync(userId);
            if (!result.IsSuccessful) return this.Error(result);

            if (result.Data is null) return this.NotFound();

            var badges = CreateBadgeCountModelCollection(result.Data);
            return Ok(badges);
        }

        [HttpGet]
        [Route("tests/{testId}")]
        public async Task<IActionResult> GetTestAll([FromRoute]int testId)
        {
            var result = await this.usersBadgesService.GetAllByTestAsync(testId);
            if (!result.IsSuccessful) return this.Error(result);

            if (result.Data is null) return this.NotFound();

            var badges = CreateBadgeCountModelCollection(result.Data);
            return Ok(badges);
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
