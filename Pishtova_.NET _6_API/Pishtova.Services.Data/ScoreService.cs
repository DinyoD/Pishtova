namespace Pishtova.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.Score;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ScoreService : IScoreService
    {
        private readonly PishtovaDbContext db;

        public ScoreService(PishtovaDbContext db)
        {
            this.db = db;
        }

        public async Task SaveScoreInDbAsync(ScoreModel model)
        {
            var score = new Score
            {
                UserId = model.UserId,
                SubjectCategoryId = model.SubjectCategoryId,
                Points = model.Points
            };
            await this.db.Scores.AddAsync(score);
            await this.db.SaveChangesAsync();
        }

        public async Task<SubjectRankingByScoresModel> GetUsersScoreBySubjectIdAsync(int subjectId)
        {
            var scoresForSubject =  await this.db.Scores
                .Include(x => x.SubjectCategory)
                .Include(x => x.User)
                .Where(x => x.SubjectCategory.SubjectId == subjectId)
                .ToListAsync();
            return GetUsersPointsForSubjectByScores(scoresForSubject);
        }

        public async Task<ICollection<SubjectPointsModel>> GetUserSubjectScoresAsync(string userId)
        {
            var scores = await this.db.Scores
                .Include(x => x.SubjectCategory)
                .ThenInclude(x => x.Subject)
                .Where(x => x.UserId == userId).ToListAsync();
            return GetSubjectPointsByScores(scores);
        }

        public async Task<ICollection<CategoryWithPointsModel>> GetSubjectCategoriesScoresAsync(string userId, int subjectId)
        {
            var scores = await this.db.Scores
                .Include(x => x.SubjectCategory)
                .ThenInclude(x => x.Subject)
                .Where(x => x.UserId == userId && x.SubjectCategory.SubjectId == subjectId).ToListAsync();
            return GetCategoriesPointsByScores(scores);
        }

        private static ICollection<CategoryWithPointsModel> GetCategoriesPointsByScores(ICollection<Score> scores)
        {
            var result = new List<CategoryWithPointsModel>();
            foreach (var item in scores)
            {
                var category = result.FirstOrDefault(x => x.Name == item.SubjectCategory.Name);

                if (category == null)
                {
                    category = new CategoryWithPointsModel
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

        private static SubjectRankingByScoresModel GetUsersPointsForSubjectByScores(ICollection<Score> scores)
        {
            var result = new SubjectRankingByScoresModel();
            foreach (var score in scores)
            {
                var user = result.UsersPointsForSubject.FirstOrDefault(x=>x.UserId == score.UserId);
                if (user == null)
                {
                    user = new UserPointsForSubjectModel { 
                        UserName = score.User.Name,
                        UserId = score.UserId,
                        Points = 0,
                        ProblemsCount = 0
                    };

                    result.UsersPointsForSubject.Add(user);
                }

                user.Points += score.Points;
                user.ProblemsCount += 1;
            }
            result.UsersPointsForSubject = result.UsersPointsForSubject.Where(x => x.ProblemsCount >= 20).ToList();
            return result;
        }

        private static ICollection<SubjectPointsModel> GetSubjectPointsByScores(ICollection<Score> scores)
        {
            var result = new List<SubjectPointsModel>();
            foreach (var item in scores)
            {
                var subject = result.FirstOrDefault(x => x.Name == item.SubjectCategory.Subject.Name);

                if (subject == null)
                {
                    subject = new SubjectPointsModel
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

    }

}
