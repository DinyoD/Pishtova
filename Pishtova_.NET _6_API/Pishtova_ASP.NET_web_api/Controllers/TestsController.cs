
namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Test;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.OperationResults;

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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] TestInputModel inputModel)
        {
            try
            {
                
                var test = new Test { 
                    UserId = inputModel.UserId,
                    SubjectId = inputModel.SubjectId,
                    Score = inputModel.Score,
                };

                var testId = await this.testService.CreateAsync(test);
                await this.SaveBadgeForTestCount(inputModel.UserId, testId);
                return StatusCode(201, new SaveTestResult { TestId = testId });
            }
            catch (System.Exception)
            {
                return StatusCode(400, new ErrorResult { Message = "Server error" });
            }
        }


        // TODO Optimaze method!!!
        private async Task SaveBadgeForTestCount(string userId, int testId)
        {
            var userTests = this.testService.GetUserTestsCount(userId);
            var testCountForBadge = new int[] { 10, 20, 50, 100 };
            if (!testCountForBadge.Contains(userTests))
            {
                return;
            }
            var badgeCode = 2000 + userTests;
            var badge = await this.badgeService.GetAllByCodeAsync(badgeCode);
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
