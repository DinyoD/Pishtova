
namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Test;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Extensions;
    using System;

    public class TestsController : ApiController
    {
        private readonly ITestService testService;
        private readonly IBadgeService badgeService;
        private readonly IUsersBadgesService usersBadgesService;

        public TestsController(
            ITestService testService, 
            IBadgeService badgeService,
            IUsersBadgesService usersBadgesService)
        {
            this.testService = testService;
            this.badgeService = badgeService;
            this.usersBadgesService = usersBadgesService;
        }


        [HttpGet("{testId:int}")]
        [HttpHead("{testId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int testId)
        {
            var getTestResult = await this.testService.GetAsync(testId);
            if (!getTestResult.IsSuccessful) return this.Error(getTestResult);

            var test = getTestResult.Data;
            if (test is null) return this.NotFound();

            return this.Ok(test);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] TestInputModel inputModel)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(inputModel)) return this.Error(operationResult);

            //TODO Validate inputModel (FluentValidation)

            //TODO Implement Mapper (AutoMapper)
            var test = new Test { 
                UserId = inputModel.UserId,
                SubjectId = inputModel.SubjectId,
                Score = inputModel.Score,
            };

            var createdTestResult = await this.testService.CreateAsync(test);
            if (!createdTestResult.IsSuccessful) return this.Error(createdTestResult);

            var createdTestId = createdTestResult.Data;
            await this.SaveBadgeForTestCount(inputModel.UserId, createdTestId);

            return this.CreatedAtAction("GetById", new { testId = createdTestId}, new { testId = createdTestId});

        }


        // TODO Optimaze method!!!
        private async Task SaveBadgeForTestCount(string userId, int testId)
        {
            var result = await this.testService.GetUserTestsCountAsync(userId);
            var userTestCount = result.Data;
            var testCountForBadge = new int[] { 10, 20, 50, 100 };
            if (!testCountForBadge.Contains(userTestCount))
            {
                return;
            }
            var badgeCode = 2000 + userTestCount;
            var badge = await this.badgeService.GetByCodeAsync(badgeCode);
            var model = new UserBadge
            {
                UserId = userId,
                TestId = testId,
                BadgeId = badge.Id
            };
            await this.usersBadgesService.CreatAsync(model);

        }
    }
}
