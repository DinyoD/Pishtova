namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.Municipality;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMunicipalityService
    {
        Task<int> CreateAsync(string name);

        Task<ICollection<MunicipalityModel>> GetAllAsync();

        Task<MunicipalityModel> GetOneByIdAsync(int id);
    }
}
