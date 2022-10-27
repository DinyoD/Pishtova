namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public class TestService : ITestService
    {
        private readonly PishtovaDbContext db;

        public TestService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<OperationResult<Test>> GetAsync(int testId)
        {
            var operationResult = new OperationResult<Test>();
            if (!operationResult.ValidateNotNull(testId)) return operationResult;

            try
            {
                var result = await this.db.Tests.Where(x => x.Id == testId).FirstOrDefaultAsync();
                operationResult.Data = result;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult<int>> CreateAsync(Test test)
        {
            var operationResult = new OperationResult<int>();
            if (!operationResult.ValidateNotNull(test)) return operationResult;

            //TODO Implement test validator - (FluentValidation)
            try
            {
                var result = await this.db.Tests.AddAsync(test);
                await this.db.SaveChangesAsync();
                operationResult.Data = result.Entity.Id;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult<int>> GetUserTestsCountAsync(string userId)
        {
            var operationResult = new OperationResult<int>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;

            try
            {
                var tests = await this.db.Tests.Where(x => x.UserId == userId).CountAsync();
                operationResult.Data = tests;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }
    }
}
