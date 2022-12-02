namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public class ProblemService : IProblemService
    {
        private readonly PishtovaDbContext db;

        public ProblemService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<ICollection<Problem>>> GenerateTest(List<int> testPattern)
        {
            var operationResult = new OperationResult<ICollection<Problem>>();
            if (!operationResult.ValidateNotNull(testPattern)) return operationResult;

            try
            {
                var result = new List<Problem>();

                foreach (var catId in testPattern)
                {
                    var problemsCount = this.db.Problems.Where(x => x.SubjectCategoryId == catId).Count();

                    var randomIndex = new Random().Next(1, problemsCount);
                    var problem = await getRandomProblem(catId, randomIndex);

                    while (result.Select(x => x.Id).ToList().Contains(problem.Id))
                    {
                        randomIndex = new Random().Next(1, problemsCount);
                        problem = await getRandomProblem(catId, randomIndex);
                    }
                    result.Add(problem);
                }
                operationResult.Data = result;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        private async Task<Problem> getRandomProblem(int catId, int randomIndex)
        {
            return await this.db.Problems
                .Where(x => x.SubjectCategoryId == catId)
                .Include(x => x.Answers)
                .Skip(randomIndex)
                .FirstAsync();
        }
    }
}
