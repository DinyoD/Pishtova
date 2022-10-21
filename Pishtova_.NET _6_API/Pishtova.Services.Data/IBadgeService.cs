namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    using Pishtova.Data.Model;

    public interface IBadgeService
    {

        // TODO Get method with filter
        Task<Badge> GetAllByCodeAsync(int badgeCode);
    }
}
