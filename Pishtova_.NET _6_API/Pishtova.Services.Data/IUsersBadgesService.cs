namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;

    public interface IUsersBadgesService
    {
        Task CreatAsync(UserBadge userBadge);


        //TODO GetAll method with filter
        Task<ICollection<UserBadge>> GetAllByUserAsync(string userId);

        //TODO GetAll method with filter
        Task<ICollection<UserBadge>> GetAllByTestAsync(int testId);
    }

}
