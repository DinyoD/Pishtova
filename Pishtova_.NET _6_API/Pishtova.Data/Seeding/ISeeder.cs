namespace Pishtova.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    public interface ISeeder
    {
        Task SeedAsync(PishtovaDbContext dbContext, IServiceProvider serviceProvider);
    }
}
