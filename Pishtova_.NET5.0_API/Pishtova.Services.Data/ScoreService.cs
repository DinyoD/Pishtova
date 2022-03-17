﻿namespace Pishtova.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.Score;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using Pishtova_ASP.NET_web_api.Model.User;
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
            return this.aggreteUsersInfo(scoresForSubject);
        }

        private SubjectRankingByScoresModel aggreteUsersInfo(List<Score> scoresForSubject)
        {
            var result = new SubjectRankingByScoresModel();
            foreach (var score in scoresForSubject)
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
    }

}
