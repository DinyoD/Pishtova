namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    public interface IBadgeService
    {

        // TODO Get method with filter
        Task<string> GetBadgeIdByCodeAsync(int badgeCode);
    }
}
