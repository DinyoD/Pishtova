namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    using Pishtova.Data.Common.Utilities;

    public interface IBadgeService
    {
        Task<OperationResult<string>> GetIdByCodeAsync(int badgeCode);
    }
}
