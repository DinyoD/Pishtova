namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Author;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using System.Collections.Generic;

    public class AuthorsController : ApiController
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }


        [HttpGet]
        [Route("[action]/{id}")]
        public ICollection<AuthorDTO> All (int id)
        {
            var result = this.authorService.GetAuthorsWithWorks(id);
            if (result == null)
            {
                return (ICollection<AuthorDTO>)BadRequest(new ErrorResult { Message = "Uncorrect Subject ID!" });
            }
            return result;
        }
    }
}
