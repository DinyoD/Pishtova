namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Test;

    public class UserStatsController: ApiController
    {
        private readonly ITestService testService;

        public UserStatsController(
            ITestService testService)
        {
            this.testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }

        [HttpGet]
        [Route("tests")]
        public async Task<IActionResult> GetTestCount([FromQuery]string userId)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(userId)) return this.Error(operationResult);

            var result = await this.testService.GetUserTestsCountAsync(userId);
            if (!result.IsSuccessful) return this.Error(result);

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("lasttests")]
        public async Task<IActionResult> GetLastTests([FromQuery] string userId, [FromQuery] int testsCount)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(userId)) return this.Error(operationResult);
            if (!operationResult.ValidateNotNull(testsCount)) return this.Error(operationResult);

            var result = await this.testService.GetUserLastByCount(userId, testsCount);
            if (!result.IsSuccessful) return this.Error(result);

            var returnedValue = result.Data
                .Select(x => new TestScoreViewModel
                {
                    SubjectName = x.Subject.Name,
                    Score = (int)(x.Score != null ? x.Score : 0),
                    Date = x.CreatedOn.ToShortDateString()
                }
                )
                .ToList();

            return Ok(returnedValue);
        }

        [HttpGet]
        [Route("lastdays")]
        public async Task<IActionResult> GetTestsByDays([FromQuery] string userId, [FromQuery] int daysCount)
        {
            var operationresult = new OperationResult();
            if (!operationresult.ValidateNotNull(userId)) return this.Error(operationresult);
            if (!operationresult.ValidateNotNull(daysCount)) return this.Error(operationresult);

            var result = await this.testService.GetUserLastByDays(userId, daysCount);
            if (!result.IsSuccessful) return this.Error(result);

            var returnedValue = this.GetDayTestsVeiewModelCollection(result.Data, daysCount);
            return Ok(returnedValue);
        }

        private ICollection<DayTestsViewModel> GetDayTestsVeiewModelCollection(ICollection<Test> tests, int daysCount)
        {
            var result = new List<DayTestsViewModel>();
            for (int i = daysCount - 1; i >= 0; i--)
            {
                result.Add(new DayTestsViewModel
                {
                    Date = DateTime.Now.Date.AddDays(-i).ToString("dd.MM.yy"),
                    TestsCount = 0
                });
            }
            foreach (var test in tests)
            {
                var day = result.FirstOrDefault(x => x.Date == test.CreatedOn.ToString("dd.MM.yy"));
                if (day != null) day.TestsCount++;
            }
            return result;
        }
    }
}

