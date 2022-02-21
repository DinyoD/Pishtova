namespace Pishtova.Services.Data
{
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using System.Threading.Tasks;

    public interface IScoreService 
    { 
  
        Task SaveScoreInDbAsync(Score score);

        Task<SubjectRankingByScores> GetUsersScoreBySubjectIdAsync (int subjectId);
    }

}
