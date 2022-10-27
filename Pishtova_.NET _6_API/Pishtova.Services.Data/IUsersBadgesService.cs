namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface IUsersBadgesService
    {
        Task<OperationResult<UserBadge>> GetById(int userBadgeId);

        Task<OperationResult<int>> CreateAsync(UserBadge userBadge);

        Task<OperationResult<ICollection<UserBadge>>> GetAllByUserAsync(string userId);

        Task<OperationResult<ICollection<UserBadge>>> GetAllByTestAsync(int testId);
    }

}
