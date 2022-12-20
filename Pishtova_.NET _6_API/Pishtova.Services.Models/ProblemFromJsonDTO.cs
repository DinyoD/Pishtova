namespace Pishtova.Services.Models
{
    public class ProblemFromJsonDTO: ProblemBaseDTO
    {
        public string CategorieName { get; set; }

        public string A { get; set; }

        public string B { get; set; }

        public string C { get; set; }

        public string D { get; set; }

        public string CorrectAnswer { get; set; }
    }
}
