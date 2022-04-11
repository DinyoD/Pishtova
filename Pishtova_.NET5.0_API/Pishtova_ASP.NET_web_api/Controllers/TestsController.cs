
namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Threading.Tasks;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.UserBadge;
    using Pishtova_ASP.NET_web_api.Model.OperationResults;

    public class TestsController : ApiController
    {
        private readonly ITestService testService;
        private readonly IUserService userService;
        private readonly IBadgeService badgeService;
        private readonly IUsersBadgesService usersBadgesService;

        public TestsController(
            ITestService testService, 
            IUserService userService,
            IBadgeService badgeService,
            IUsersBadgesService usersBadgesService)
        {
            this.testService = testService;
            this.userService = userService;
            this.badgeService = badgeService;
            this.usersBadgesService = usersBadgesService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Save([FromBody] int subjectId)
        {
            try
            {
                var userId =await this.userService.GetUserIdAsync(User);
                var testId = await this.testService.CreateTestAsync(userId, subjectId);
                await this.SaveBadgeForTestCount(userId, testId);
                return StatusCode(200, new SaveTestResult { TestId = testId });
            }
            catch (System.Exception)
            {
                return StatusCode(400, new ErrorResult { Message = "Server error" });
            }
        }

        private async Task SaveBadgeForTestCount(string userId, int testId)
        {
            var userTest = this.testService.GetUserTestCount(userId);
            var testCountForBadge = new int[] { 10, 20, 50, 100 };
            if (!testCountForBadge.Contains(userTest))
            {
                return;
            }
            var badgeCode = 2000 + userTest;
            var badgeId = await this.badgeService.GetBadgeIdByCodeAsync(badgeCode);
            var model = new UserBadgeModel
            {
                UserId = userId,
                TestId = testId,
                BadgeId = badgeId
            };
            await this.usersBadgesService.CreateUserBadgeAsync(model);

        }
    }
}
