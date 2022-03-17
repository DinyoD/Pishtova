namespace Pishtova_ASP.NET_web_api.Model.Author
{
    using Pishtova_ASP.NET_web_api.Model.Work;
    using System.Collections.Generic;

    public class AuthorDTO
    {
        public string Name { get; set; }

        public int Index { get; set; }

        public ICollection<WorkModel> Works { get; set; }
    }
}
