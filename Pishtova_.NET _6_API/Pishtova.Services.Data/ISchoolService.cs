namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Model.School;

    public interface ISchoolService
    {
        Task<OperationResult<ICollection<School>>> GetAllByTownIdAsync(int townId);
    }

}
