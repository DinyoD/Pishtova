namespace Pishtova.Services.Data
{
    using Pishtova.Data.Model;
    using System.Threading.Tasks;

    public interface IBadgeService
    {
        Task<string> GetBadgeIdByCodeAsync(int badgeCode);
    }
}
