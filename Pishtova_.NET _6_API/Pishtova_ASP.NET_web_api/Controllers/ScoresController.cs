namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Score;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using Pishtova_ASP.NET_web_api.Model.Category;

    public class ScoresController: ApiController
    {
        private readonly IScoreService scoreService;
        private readonly IUserService userService;

        public ScoresController(
            IScoreService scoreService,
            IUserService userService)
        {
            this.scoreService = scoreService;
            this.userService = userService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await this.scoreService.GetByIdAsync(id);
            if (!result.IsSuccessful) return this.Error(result);

            var userBadge = result.Data;
            if (userBadge is null) return this.NotFound();

            //TODO implement and return ViewModel
            return this.Ok(userBadge);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ScoreInputModel inputModel)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(inputModel)) return this.Error(operationResult);

            var score = new Score
            {
                UserId = inputModel.UserId,
                SubjectCategoryId = inputModel.SubjectCategoryId,
                Points = inputModel.Points
            };
            var result = await this.scoreService.CreateAsync(score);
            if (!result.IsSuccessful) return this.Error(result);


            return CreatedAtAction("GetById", new {Id = result.Data}, new { Id = result.Data });
        }

        [HttpGet]
        [Route("subjects")]
        public async Task<IActionResult> UserPointsBySubject([FromQuery]string userId)
        {
            var operationResult = new OperationResult<ICollection<SubjectScoreModel>>();
            if (!operationResult.ValidateNotNull(userId)) return this.Error(operationResult);

            var scoresResult =  await this.scoreService.GetUserScoresBySubjectsAsync(userId);
            if (!scoresResult.IsSuccessful) return this.Error(scoresResult);

            var result = this.GetSubjectsScores(scoresResult.Data);
            return Ok(result);
        }

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> SubjectPointsByCategories([FromQuery]string userId, [FromQuery]int subjectId)
        {
            var operationResult = new OperationResult<ICollection<CategoryScoreModel>>();
            if (!operationResult.ValidateNotNull(userId)) return this.Error(operationResult);
            if (!operationResult.ValidateNotNull(subjectId)) return this.Error(operationResult);

            var scoresResult =  await this.scoreService.GetUserScoresBySubjectCategoriesAsync(userId, subjectId);
            if (!scoresResult.IsSuccessful) return this.Error(scoresResult);

            var result = this.GetCategoriesScores(scoresResult.Data);
            return Ok(result);
        }

        private ICollection<SubjectScoreModel> GetSubjectsScores(ICollection<Score> scores)
        {
            var result = new List<SubjectScoreModel>();
            foreach (var item in scores)
            {
                var subject = result.FirstOrDefault(x => x.Name == item.SubjectCategory.Subject.Name);

                if (subject == null)
                {
                    subject = new SubjectScoreModel
                    {
                        Name = item.SubjectCategory.Subject.Name,
                        Id = item.SubjectCategory.SubjectId
                    };
                    result.Add(subject);
                }
                subject.Problems += 1;
                subject.Points += item.Points;
            }
            return result;
        }

        private ICollection<CategoryScoreModel> GetCategoriesScores(ICollection<Score> scores)
        {
            var result = new List<CategoryScoreModel>();
            foreach (var item in scores)
            {
                var category = result.FirstOrDefault(x => x.Name == item.SubjectCategory.Name);

                if (category == null)
                {
                    category = new CategoryScoreModel
                    {
                        Name = item.SubjectCategory.Name
                    };
                    result.Add(category);
                }
                category.Problems += 1;
                category.Points += item.Points;
            }
            return result;
        }
    }
}
