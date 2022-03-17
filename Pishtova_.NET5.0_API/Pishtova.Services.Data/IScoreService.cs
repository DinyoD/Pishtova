namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.Score;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using System.Threading.Tasks;

    public interface IScoreService 
    { 
  
        Task SaveScoreInDbAsync(ScoreModel model);

        Task<SubjectRankingByScoresModel> GetUsersScoreBySubjectIdAsync (int subjectId);
    }

}
