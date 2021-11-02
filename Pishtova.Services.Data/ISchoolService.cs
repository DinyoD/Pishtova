namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.School;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISchoolService
    {
        Task CreateAsync(string name, int townId);

        Task<ICollection<SchoolModel>> GetAllByTownId(int townId);
    }

}
