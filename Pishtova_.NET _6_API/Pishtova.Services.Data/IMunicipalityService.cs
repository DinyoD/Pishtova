namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface IMunicipalityService
    {
        Task<OperationResult<Municipality>> GetByIdAsync(int id);

        Task<OperationResult<ICollection<Municipality>>> GetAllAsync();
    }
}
