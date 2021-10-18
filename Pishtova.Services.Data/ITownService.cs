namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    public interface ITownService
    {
        Task<int> CreateAsync(string name, int municipalityId);
    }
}
