namespace Pishtova.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Model.Author;

    public interface IAuthorService
    {
        Task<OperationResult<ICollection<AuthorModel>>> GetAuthorsWithWorksAsync(int subjectId);
    }
}
