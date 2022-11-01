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
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using Pishtova_ASP.NET_web_api.Model.User;

    public class UserStatsController: ApiController
    {
        private readonly ITestService testService;
        private readonly ISubjectService subjectService;
        private readonly IScoreService scoreService;

        public UserStatsController(
            ITestService testService, 
            ISubjectService subjectService,
            IScoreService scoreService)
        {
            this.testService = testService ?? throw new ArgumentNullException(nameof(testService));
            this.subjectService = subjectService ?? throw new ArgumentNullException(nameof(subjectService));
            this.scoreService = scoreService ?? throw new ArgumentNullException(nameof(scoreService));
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
        public async Task<IActionResult> GetLastTests([FromQuery]string userId, [FromQuery]int testsCount)
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
        public async Task<IActionResult> GetTestsByDays([FromQuery]string userId, [FromQuery]int daysCount)
        {
            var operationresult = new OperationResult();
            if (!operationresult.ValidateNotNull(userId)) return this.Error(operationresult);
            if (!operationresult.ValidateNotNull(daysCount)) return this.Error(operationresult);

            var result = await this.testService.GetUserLastByDays(userId, daysCount);
            if (!result.IsSuccessful) return this.Error(result);

            var returnedValue = this.GetDayTestsVeiewModelCollection(result.Data, daysCount);
            return Ok(returnedValue);
        }

        [HttpGet]
        [Route("bestrank")]
        public async Task<IActionResult> GetUserBestRank([FromQuery]string userId)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(userId)) return this.Error(operationResult);

            var result = await this.subjectService.GetAllAsync();
            if (!result.IsSuccessful) return this.Error(result);

            var subjectDTOCollection = result.Data.Select(x => new SubjectBaseModel { Id = x.Id, Name = x.Name }).ToList();

            var userBestRank = await this.GetUserBestRank(subjectDTOCollection, userId);
            if (!userBestRank.IsSuccessful) return this.Error(userBestRank);

            return Ok(userBestRank.Data);
        }

        [HttpGet]
        [Route("subjectrank")]
        public async Task<IActionResult> Ranking([FromQuery]int subjectId)
        {
            var operationresult = new OperationResult();
            if (!operationresult.ValidateNotNull(subjectId)) return this.Error(operationresult);

            var getScoresOperation = await this.scoreService.GetSubjectScoresAsync(subjectId);
            if (!getScoresOperation.IsSuccessful) return this.Error(getScoresOperation);

            var result = this.GetSubjectRanking(getScoresOperation.Data);

            return Ok(result);
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

        private ICollection<UserScoreBySubjectModel> GetSubjectRanking(ICollection<Score> scores)
        {
            var result = new List<UserScoreBySubjectModel>();
            foreach (var score in scores)
            {
                var user = result.FirstOrDefault(x => x.UserId == score.UserId);
                if (user == null)
                {
                    user = new UserScoreBySubjectModel
                    {
                        UserName = score.User.Name,
                        UserId = score.UserId,
                        Points = 0,
                        ProblemsCount = 0
                    };

                    result.Add(user);
                }

                user.Points += score.Points;
                user.ProblemsCount += 1;
            }
            result = result.Where(x => x.ProblemsCount >= 20).ToList();
            return result;
        }

        private async Task<OperationResult<UserRankModel>> GetUserBestRank(ICollection<SubjectBaseModel> subjects, string userId)
        {
            var operationResult = new OperationResult<UserRankModel>();
            if (!operationResult.ValidateNotNull(subjects)) return operationResult;
            if (!operationResult.ValidateNotNull(userId)) return operationResult;

            var result = new UserRankModel
            {
                Rank = int.MaxValue
            };

            foreach (var sbj in subjects)
            {
                var a = await this.GetUserSubjectRank(userId, sbj.Id);
                if (!a.IsSuccessful) operationResult.AppendErrors(a);

                if (result.Rank > a.Data)
                {
                    result.Rank = a.Data;
                    result.SubjectName = sbj.Name;
                }
            }
            return operationResult.WithData(result); 
        }

        private async Task<OperationResult<int>> GetUserSubjectRank(string userId, int subjectId)
        {
            var operationResult = new OperationResult<int>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;
            if (!operationResult.ValidateNotNull(subjectId)) return operationResult;

            var getScoresOperation = await this.scoreService.GetSubjectScoresAsync(subjectId);
            if (!getScoresOperation.IsSuccessful) operationResult.AppendErrors(getScoresOperation);

            var usersScore = this.GetSubjectRanking(getScoresOperation.Data);
            var usersScoreOrderedDesc = usersScore.OrderByDescending(x => (double)((double)x.Points /(double)x.ProblemsCount)).ToList();
            var result = usersScoreOrderedDesc.FindIndex(x => x.UserId == userId);
            result++;
            return operationResult.WithData(result);
        }
    }

}
