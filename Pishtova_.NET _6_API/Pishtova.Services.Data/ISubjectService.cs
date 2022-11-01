namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Common.Utilities;
    using Pishtova.Data.Model;

    public interface ISubjectService
    {
        Task<OperationResult<ICollection<Subject>>> GetAllAsync();

        Task<OperationResult<Subject>> GetByIdAsync(int id);
    }
}
