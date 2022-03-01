namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.Author;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        List<AuthorModel> GetAuthorsWithWorks(int subjectId);
    }
}
