namespace Pishtova.Data.Model
{
    using Pishtova.Data.Common.Model;

    public class SubjectCategory : BaseDeletableModel<int> 
    {
        public string Name { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
    }
}
