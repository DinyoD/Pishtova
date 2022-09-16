namespace Pishtova.Services.Data
{
    using Pishtova.Data;
    using System.Threading.Tasks;
    using Pishtova.Data.Model;
    using System.Linq;

    public class TestService : ITestService
    {
        private readonly PishtovaDbContext db;

        public TestService(PishtovaDbContext db)
        {
            this.db = db;
        }

        public async Task<int> CreateTestAsync(string userId, int subjectId)
        {
            var result = await this.db.Tests.AddAsync(new Test { UserId = userId, SubjectId = subjectId});
            await this.db.SaveChangesAsync();
            return result.Entity.Id;
        }

        public int GetUserTestCount(string userId)
        {
            return this.db.Tests.Where(x => x.UserId == userId).Count();
        }
    }
}
