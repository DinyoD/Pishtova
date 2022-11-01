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

    public class ScoreService : IScoreService
    {
        private readonly PishtovaDbContext db;

        public ScoreService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<Score>> GetByIdAsync(int scoreId)
        {
            var operationResult = new OperationResult<Score>();
            if (!operationResult.ValidateNotNull(scoreId)) return operationResult;

            try
            {
                var result = await this.db.Scores.Where(x => x.Id == scoreId).FirstOrDefaultAsync();
                operationResult.Data = result;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult<int>> CreateAsync(Score score)
        {
            var operationresult = new OperationResult<int>();
            if (!operationresult.ValidateNotNull(score)) return operationresult;

            try
            {
                var saved = await this.db.Scores.AddAsync(score);
                await this.db.SaveChangesAsync();
                operationresult.Data = saved.Entity.Id;
            }
            catch (Exception e)
            {
                operationresult.AddException(e);
            }
            return operationresult;
        }

        public async Task<OperationResult<ICollection<Score>>> GetSubjectScoresAsync(int subjectId)
        {
            var operationresult = new OperationResult<ICollection<Score>>();
            if (!operationresult.ValidateNotNull(subjectId)) return operationresult;

            try
            {
                var score =  await this.db.Scores
                    .Include(x => x.SubjectCategory)
                    .Include(x => x.User)
                    .Where(x => x.SubjectCategory.SubjectId == subjectId)
                    .ToListAsync();
                operationresult.Data = score;

            }
            catch (Exception e)
            {
                operationresult.AddException(e);
            }
            return operationresult;
        }

        public async Task<OperationResult<ICollection<Score>>> GetUserScoresBySubjectsAsync(string userId)
        {
            
            var operationresult = new OperationResult<ICollection<Score>>();
            if (!operationresult.ValidateNotNull(userId)) return operationresult;

            try
            {
                var scores = await this.db.Scores
                    .Include(x => x.SubjectCategory)
                    .ThenInclude(x => x.Subject)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
                operationresult.Data = scores;

            }
            catch (Exception e)
            {
                operationresult.AddException(e);
            }
            return operationresult;
        }

        public async Task<OperationResult<ICollection<Score>>> GetUserScoresBySubjectCategoriesAsync(string userId, int subjectId)
        {
            
            var operationresult = new OperationResult<ICollection<Score>>();
            if (!operationresult.ValidateNotNull(subjectId)) return operationresult;

            try
            {
                var scores = await this.db.Scores
                .Include(x => x.SubjectCategory)
                .ThenInclude(x => x.Subject)
                .Where(x => x.UserId == userId && x.SubjectCategory.SubjectId == subjectId)
                .ToListAsync();
                operationresult.Data = scores;

            }
            catch (Exception e)
            {
                operationresult.AddException(e);
            }
            return operationresult;
        }

    }

}
