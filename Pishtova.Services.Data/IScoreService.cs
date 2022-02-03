namespace Pishtova.Services.Data
{
    using Pishtova.Data.Model;
    using System.Threading.Tasks;

    public interface IScoreService
    {
        Task SaveScoreInDbAsync(Score score);
    }

}
