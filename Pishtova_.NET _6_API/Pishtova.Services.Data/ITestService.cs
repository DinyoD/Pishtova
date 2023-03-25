namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface ITestService
    {
        /// <summary>
        /// Asynchronously find an entity(Test) with the given primary key(Id) value.
        /// </summary>
        /// <param name="id">The values of the primary key(Id) for the entity to be found.</param>
        /// <returns>A task whose result contains OperationResult object with Data property - founded entity(Test). If no entity is found, then
        ///     Data is null</returns>
        Task<OperationResult<Test>> GetBtIdAsync(int id);
        /// <summary>
        /// Asynchronously save entity(Test) in Db.
        /// </summary>
        /// <param name="test"></param>
        /// <returns>A task whose result contains OperationResult object with Data property - primary key(Id) value of the newly created test</returns>
        Task<OperationResult<int>> CreateAsync(Test test);
        /// <summary>
        /// Asynchronously get count of completed test for a user.
        /// </summary>
        /// <param name="userId">The unique user Id.</param>
        /// <returns>A task whose result contains OperationResult object with Data property - count of all completed tests for a user.</returns>
        Task<OperationResult<int>> GetUserTestsCountAsync(string id);

        public Task<OperationResult<ICollection<Test>>> GetUserLastByCountAsync(string id, int testCount);

        public Task<OperationResult<ICollection<Test>>> GetUserLastByDaysAsync(string id, int daysCount);

        Task<OperationResult<int>> DeleteByUserIdAsync(string userId);
    }
}
