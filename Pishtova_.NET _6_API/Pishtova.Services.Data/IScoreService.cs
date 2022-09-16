namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.Score;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IScoreService 
    { 
  
        Task SaveScoreInDbAsync(ScoreModel model);

        Task<SubjectRankingByScoresModel> GetUsersScoreBySubjectIdAsync (int subjectId);

        Task<ICollection<SubjectPointsModel>> GetUserSubjectScoresAsync(string userId);

        Task<ICollection<CategoryWithPointsModel>> GetSubjectCategoriesScoresAsync(string userId, int subjectId);
    }

}
