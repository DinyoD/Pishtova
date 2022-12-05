namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;
    using System;

    public class Work: BaseDeletableModel<string>
    {
        public Work()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public int Index { get; set; }

        public string  AuthorId { get; set; }

        public Author Author { get; set; }

        public string SubjectId { get; set; }

        public Subject Subject { get; set; }
    }
}
