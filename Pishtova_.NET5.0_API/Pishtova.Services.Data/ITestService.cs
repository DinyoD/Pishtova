namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    public interface ITestService
    {
        Task<int> CreateTestAsync(string userId, int subjectId);
    }
}
