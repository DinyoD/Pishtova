namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Test;
    using Pishtova_ASP.NET_web_api.Model.Answer;
    using Pishtova_ASP.NET_web_api.Model.Problem;

    public class ProblemsController: ApiController
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [HttpGet]
        [Route("subject/{id}")]
        public async Task<IActionResult> generateTest(int id)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(id)) return this.Error(operationResult);

            try
            {
                var testPattern = new Pattern().ProblemsSubjectIDs[id];
                if (testPattern == null || testPattern.Count == 0) return this.NotFound();

                var result = await this.problemService.GenerateTest(testPattern);
                if (!result.IsSuccessful) return this.Error(result);

                var problems = result.Data;
                if (problems == null) return this.NotFound();

                var test = problems.Select(this.ToProblemModel).ToList();
                return this.Ok(test);
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
                return this.Error(operationResult); 
            }

        }

        private ProblemModel ToProblemModel(Problem problem)
        {
            return new ProblemModel
            {
                Id = problem.Id,
                PictureUrl = problem.PictureUrl == "empty" ? null : problem.PictureUrl,
                Hint = problem.Hint,
                SubjectCategoryId = problem.SubjectCategoryId,
                QuestionText = problem.QuestionText,
                Answers = problem.Answers
                                    .Select( x => new AnswerModel 
                                        { 
                                            Id = x.Id,
                                            IsCorrect = x.IsCorrect,
                                            Text = x.Text,
                                        })
                                    .ToList()
            };
        }
    }
}
