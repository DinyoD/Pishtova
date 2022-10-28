
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
    using System.Collections;
    using System.Collections.Generic;

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
            this.testService = testService ?? throw new ArgumentNullException(nameof(testService));
            this.badgeService = badgeService ?? throw new ArgumentNullException(nameof(badgeService));
            this.usersBadgesService = usersBadgesService ?? throw new ArgumentNullException(nameof(usersBadgesService));
        }


        [HttpGet("{testId:int}")]
        [HttpHead("{testId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int testId)
        {
            var result = await this.testService.GetBtIdAsync(testId);
            if (!result.IsSuccessful) return this.Error(result);

            var test = result.Data;
            if (test is null) return this.NotFound();

            //TODO implement and return ViewModel
            return this.Ok(test);
        }

        [HttpPost]
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

            var saveBadgeResult = await this.SaveBadgeForTestCount(inputModel.UserId, createdTestId);
            if (!saveBadgeResult.IsSuccessful) return this.Error(saveBadgeResult);

            return this.CreatedAtAction("GetById", new { testId = createdTestId }, new TestBasicViewModel { TestId = createdTestId });

        }

        [HttpGet]
        [Route("users/{userId}/count")]
        public async Task<IActionResult> GetCountByUser(string userId)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(userId)) return this.Error(operationResult);

            var result = await this.testService.GetUserTestsCountAsync(userId);
            if (!result.IsSuccessful) return this.Error(result);

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<IActionResult> GetLastByUser([FromRoute]string userId, [FromQuery]int testsCount, [FromQuery] int daysCount)
        {
            if (testsCount != 0) return await this.GetLastByTestsCount(userId, testsCount);
            if (daysCount != 0) return await this.GetLastByDaysCount(userId, daysCount);
            return Ok();
        }

        // TODO Optimaze method!!!
        private async Task<OperationResult<bool>> SaveBadgeForTestCount(string userId, int testId)
        {
            var operationResult = new OperationResult<bool>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;
            if (!operationResult.ValidateNotNull(testId)) return operationResult;

            var result = await this.testService.GetUserTestsCountAsync(userId);
            if (!result.IsSuccessful) return operationResult.AppendErrors(result);

            var userTestCount = result.Data;
            var testCountForBadge = new int[] { 10, 20, 50, 100 };
            if (!testCountForBadge.Contains(userTestCount))
            {
                return operationResult.WithData(false);
            }
            var badgeCode = 2000 + userTestCount;

            var badgeIdResult = await this.badgeService.GetByCodeAsync(badgeCode);
            if (!badgeIdResult.IsSuccessful) return operationResult.AppendErrors(badgeIdResult);

            var model = new UserBadge
            {
                UserId = userId,
                TestId = testId,
                BadgeId = badgeIdResult.Data.Id
            };
            var createBadgeResult = await this.usersBadgesService.CreateAsync(model);
            if (!createBadgeResult.IsSuccessful) return operationResult.AppendErrors(createBadgeResult);

            return operationResult.WithData(true);
        }

        private async Task<IActionResult> GetLastByTestsCount(string userId, int testsCount)
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
                    CreatedOn = x.CreatedOn.ToShortDateString()
                }
                )
                .ToList();

            return Ok(returnedValue);
        }

        private async Task<IActionResult> GetLastByDaysCount(string userId, int daysCount)
        {
            var operationresult = new OperationResult();
            if (!operationresult.ValidateNotNull(userId)) return this.Error(operationresult);
            if (!operationresult.ValidateNotNull(daysCount)) return this.Error(operationresult);

            var result = await this.testService.GetUserLastByDays(userId, daysCount);
            if (!result.IsSuccessful) return this.Error(result);

            var returnedValue = this.GetDayTestsVeiewModelCollection(result.Data);
            return Ok(returnedValue);
        }

        private ICollection<DayTestsViewModel> GetDayTestsVeiewModelCollection(ICollection<Test> tests)
        {
            var result = new List<DayTestsViewModel>();
            foreach (var test in tests)
            {
                var day = result.FirstOrDefault(x => x.Date == test.CreatedOn.ToString("dd.MM.yy"));
                if (day == null)
                {
                    day = new DayTestsViewModel
                    {
                        Date = test.CreatedOn.ToString("dd.MM.yy"),
                        TestCount = 0
                    };
                    result.Add(day);    
                }
                day.TestCount++;
            }
            return result;
        }
    }
}