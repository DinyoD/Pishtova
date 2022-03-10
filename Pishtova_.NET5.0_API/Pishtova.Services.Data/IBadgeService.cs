namespace Pishtova.Services.Data
{
    using Pishtova.Data.Model;
    using System.Threading.Tasks;

    public interface IBadgeService
    {
        Task<Badge> GetBadgeByCodeAsync(int badgeCode);
    }
}
