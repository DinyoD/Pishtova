namespace Pishtova.Services.Data
{
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using System.Threading.Tasks;

    public class ScoreService : IScoreService
    {
        private readonly PishtovaDbContext db;

        public ScoreService(PishtovaDbContext db)
        {
            this.db = db;
        }
        public async Task SaveScoreInDbAsync(Score score)
        {
            await this.db.Scores.AddAsync(score);
            await this.db.SaveChangesAsync();
        }
    }

}
