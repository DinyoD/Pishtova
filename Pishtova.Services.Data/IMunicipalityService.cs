namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    public interface IMunicipalityService
    {
        Task<int> CreateAsync(string name);
    }
}
