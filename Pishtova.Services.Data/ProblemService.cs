namespace Pishtova.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova_ASP.NET_web_api.Model.Answer;
    using Pishtova_ASP.NET_web_api.Model.Problem;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProblemService : IProblemService
    {
        private readonly PishtovaDbContext db;

        public ProblemService(PishtovaDbContext db)
        {
            this.db = db;
        }
        public async Task<ICollection<ProblemModel>> GenerateTest(List<int> testPattern)
        {
            var result = new List<ProblemModel>();

            foreach (var catId in testPattern)
            {
                var problemsCount = this.db.Problems.Where(x => x.SubjectCategoryId == catId).Count();
                var problem = await getRandomProblem(catId, new Random().Next(1, problemsCount));
                while(result.Select(x => x.Id).ToList().Contains(problem.Id))
                {
                    problem = await getRandomProblem(catId, new Random().Next(1, problemsCount));
                }
                result.Add(problem);
            }

            return result;
        }

        private async Task<ProblemModel> getRandomProblem(int catId, int randomIndex)
        {
            return await this.db.Problems
                .Where(x => x.SubjectCategoryId == catId)
                .Skip(randomIndex)
                .Select(x => new ProblemModel
                {
                    Id = x.Id,
                    PictureUrl = x.PictureUrl == "empty" ? null : x.PictureUrl,
                    Hint = x.Hint,
                    QuestionText = x.QuestionText,
                    SubjectCategoryId = x.SubjectCategoryId,
                    Answers = x.Answers.Select(a => new AnswerModel
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect
                    }).ToList(),
                })
                .FirstAsync();
        }
    }
}
