namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface IScoreService 
    {
        Task<OperationResult<Score>> GetByIdAsync(int scoreId);

        Task<OperationResult<int>> CreateAsync(Score score);

        Task<OperationResult<ICollection<Score>>> GetSubjectScoresAsync (string subjectId);

        Task<OperationResult<ICollection<Score>>> GetUserScoresBySubjectsAsync(string userId);

        Task<OperationResult<ICollection<Score>>> GetUserScoresBySubjectCategoriesAsync(string userId, string subjectId);

        Task<OperationResult<int>> DeleteByUserIdAsync(string userId);
    }

}
