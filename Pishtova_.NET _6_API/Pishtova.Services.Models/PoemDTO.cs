namespace Pishtova.Services.Models
{
    public class PoemDTO
    {
        public string Name { get; set; }

        public string TextUrl { get; set; }

        public string AnalysisUrl { get; set; }

        public string ExtrasUrl { get; set; }

        public AuthorDTO AuthorDTO { get; set; }
    }
}