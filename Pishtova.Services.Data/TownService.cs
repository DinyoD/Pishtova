namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    using Pishtova.Data;
    using Pishtova.Data.Model;

    public class TownService : ITownService
    {
        private readonly PishtovaDbContext db;

        public TownService(PishtovaDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string name)
        {
            if (name != null)
            {
                await db.Towns.AddAsync(new Town { Name = name });
                await db.SaveChangesAsync();
            }
            
        }
    }
}
