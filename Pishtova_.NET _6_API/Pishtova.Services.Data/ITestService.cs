namespace Pishtova.Services.Data
{
    using Pishtova.Data.Common.Utilities;
    using Pishtova.Data.Model;
    using System.Threading.Tasks;

    public interface ITestService
    {
        Task<OperationResult<Test>> GetAsync(int testId);

        Task<OperationResult<int>> CreateAsync(Test test);

        Task<OperationResult<int>> GetUserTestsCountAsync(string userId);
    }
}
