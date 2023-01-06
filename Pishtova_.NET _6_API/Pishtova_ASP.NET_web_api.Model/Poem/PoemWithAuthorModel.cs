namespace Pishtova_ASP.NET_web_api.Model.Poem
{
    public class PoemWithAuthorModel: PoemBaseModel
    {
        public string AuthorName { get; set; }

        public string AuthorPictureUrl { get; set; }
    }
}
