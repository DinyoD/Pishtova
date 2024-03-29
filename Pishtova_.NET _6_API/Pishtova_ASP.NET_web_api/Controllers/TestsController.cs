﻿namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Test;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Extensions;

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

            var saveBadgeForScoreResult = await this.SaveBadgeForTestScore(inputModel.UserId, createdTestId, inputModel.Score);
            if (!saveBadgeForScoreResult.IsSuccessful) return this.Error(saveBadgeForScoreResult);

            var saveBadgeForTestsCountResult = await this.SaveBadgeForTestCount(inputModel.UserId, createdTestId);
            if (!saveBadgeForTestsCountResult.IsSuccessful) return this.Error(saveBadgeForTestsCountResult);

            return this.CreatedAtAction("GetById", new { testId = createdTestId }, new TestBasicViewModel { TestId = createdTestId });

        }

        [HttpGet]
        [Route("users/{userId}/count")]
        public async Task<IActionResult> GetCountByUser(string userId)
        {
            var result = await this.testService.GetUserTestsCountAsync(userId);
            if (!result.IsSuccessful) return this.Error(result);

            return Ok(result.Data);
        }

        // TODO Optimaze method!!!
        private async Task<OperationResult<bool>> SaveBadgeForTestCount(string userId, int testId)
        {
            var operationResult = new OperationResult<bool>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;
            if (!operationResult.ValidateNotNull(testId)) return operationResult;

            var getBadgeCodeResult = await this.GetBadgeCodeByUserTestCount(userId);
            if (!getBadgeCodeResult.IsSuccessful) return operationResult.AppendErrors(getBadgeCodeResult);

            var badgeCode = getBadgeCodeResult.Data;

            var saveResult = await this.SaveBadge(userId, testId, badgeCode);
            if (!saveResult.IsSuccessful) return operationResult.AppendErrors(saveResult);

            return operationResult.WithData(true);
        }

        private async Task<OperationResult<bool>> SaveBadgeForTestScore(string userId, int testId, int score)
        {
            var operationResult = new OperationResult<bool>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;
            if (!operationResult.ValidateNotNull(testId)) return operationResult;
            if (!operationResult.ValidateNotNull(score)) return operationResult;

            if (score < 70) return operationResult.WithData(false);

            var badgeCode = this.GetBadgeCodeByScore(score);
            if (badgeCode == 0) return operationResult.WithData(false);

            var saveResult = await this.SaveBadge(userId, testId, badgeCode);
            if (!saveResult.IsSuccessful) return operationResult.AppendErrors(saveResult);
            
            return operationResult.WithData(true); 
        }

        private async Task<OperationResult<bool>> SaveBadge(string userId, int testId, int badgeCode) {

            var operationResult = new OperationResult<bool>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;
            if (!operationResult.ValidateNotNull(testId)) return operationResult;
            if (!operationResult.ValidateNotNull(badgeCode)) return operationResult;

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

        private async Task<OperationResult<int>> GetBadgeCodeByUserTestCount(string userId)
        {
            var operationResult = new OperationResult<int>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;

            var result = await this.testService.GetUserTestsCountAsync(userId);
            if (!result.IsSuccessful) return operationResult.AppendErrors(result);

            var userTestCount = result.Data;
            var testCountForBadge = new int[] { 10, 20, 50, 100 };
            if (!testCountForBadge.Contains(userTestCount))
            {
                return operationResult.WithData(0);
            }
            var badgeCode = 2000 + userTestCount;
            return operationResult.WithData(badgeCode);
        }

        private int GetBadgeCodeByScore(int score)
        {
            if (score == 100) return 1100;
            else if (score >= 90) return 1090;
            else if (score >= 80) return 1080;
            else if (score >= 70) return 1070;
            return 0;
        }
    }
}