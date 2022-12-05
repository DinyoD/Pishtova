namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface ISubjectService
    {
        Task<OperationResult<ICollection<Subject>>> GetAllAsync();

        Task<OperationResult<Subject>> GetByIdAsync(string id);
    }
}
