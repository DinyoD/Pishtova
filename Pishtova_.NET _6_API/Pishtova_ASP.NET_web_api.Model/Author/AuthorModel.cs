namespace Pishtova_ASP.NET_web_api.Model.Author
{
    using System.Collections.Generic;
    using Pishtova_ASP.NET_web_api.Model.Work;

    public class AuthorModel
    {
        public string Name { get; set; }

        public int Index { get; set; }

        public ICollection<WorkModel> Works { get; set; }
    }
}
