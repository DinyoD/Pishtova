namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.Town;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITownService
    {
        Task<int> CreateAsync(string name, int municipalityId);

        Task<ICollection<TownDTO>> GetAllByMunicipalityId(int municipalityId);
    }
}
