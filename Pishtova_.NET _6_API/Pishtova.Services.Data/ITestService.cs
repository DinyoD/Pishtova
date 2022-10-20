namespace Pishtova.Services.Data
{
    using Pishtova.Data.Model;
    using System.Threading.Tasks;

    public interface ITestService
    {
        Task<int> CreateAsync(Test test);


        // TODO GetAll method with filters
        int GetUserTestsCount(string userId);
    }
}
