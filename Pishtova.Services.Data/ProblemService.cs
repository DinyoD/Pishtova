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
        public async Task<ICollection<ProblemModel>> GenerateTest(Dictionary<int, int> testPattern)
        {
            var result = new List<ProblemModel>();

            foreach (var kvp in testPattern)
            {
                var problemsCount = this.db.Problems.Where(x => x.SubjectCategoryId == kvp.Key).Count();
                var randomIndex = new Random().Next(1, problemsCount-kvp.Value);
                var problems = await this.db.Problems
                    .Where(x => x.SubjectCategoryId == kvp.Key)
                    .Skip(randomIndex).Take(kvp.Value)
                    .Select(x=> new ProblemModel { 
                        Id = x.Id,
                        PictureUrl = x.PictureUrl,
                        Hint = x.Hint,
                        QuestionText =x.QuestionText,
                        SubjectCategoryId = x.SubjectCategoryId,
                        Answers = x.Answers.Select(a => new AnswerModel {
                            Id =a.Id,
                            Text = a.Text,
                            IsCorrect = a.IsCorrect
                            }).ToList(),
                    }).ToListAsync();
                result.AddRange(problems);
            }

            return result;
        }
    }
}
