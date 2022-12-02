namespace Pishtova.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pishtova.Data.Common.Utilities;
    using Pishtova.Data.Model;

    public interface IAuthorService
    {
        Task<OperationResult<ICollection<Author>>> GetAuthorsWithWorksBySubjectIdAsync(int subjectId);
    }
}
