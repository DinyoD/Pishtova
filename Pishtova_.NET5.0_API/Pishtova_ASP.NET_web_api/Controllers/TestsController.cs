using Microsoft.AspNetCore.Mvc;
using Pishtova.Services.Data;
using Pishtova_ASP.NET_web_api.Model.OperationResults;
using Pishtova_ASP.NET_web_api.Model.Results;
using System.Threading.Tasks;

namespace Pishtova_ASP.NET_web_api.Controllers
{
    public class TestsController: ApiController
    {
        private readonly ITestService testService;
        private readonly IUserService userService;

        public TestsController(ITestService testService, IUserService userService)
        {
            this.testService = testService;
            this.userService = userService;
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Save ([FromBody] int subjectId)
        {
            try
            {
                var userId = this.userService.GetUserId(User);
                var testId = await this.testService.CreateTestAsync(userId, subjectId);
                return StatusCode(200, new SaveTestReult { TestId = testId });
            }
            catch (System.Exception)
            {
                return StatusCode(400, new ErrorResult { Message = "Server error" });
            }
        }
    }
}
