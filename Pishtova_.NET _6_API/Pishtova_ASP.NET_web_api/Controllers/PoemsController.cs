namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Poem;

    public class PoemsController: ApiController
    {
        private readonly IPoemService poemService;

        public PoemsController(IPoemService poemService)
        {
            this.poemService = poemService ?? throw new System.ArgumentNullException(nameof(poemService));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getByid([FromRoute] string id)
        {
            var result = await this.poemService.GetById(id);
            if (!result.IsSuccessful) return this.Error(result);

            var poem = this.ToPoemModel(result.Data);

            return this.Ok(poem);

        }

        [HttpGet]
        [Route("theme/{themeId}")]
        public async Task<IActionResult> getPoemsByThemeId([FromRoute] string themeId)
        {
            var result = await this.poemService.GetAllByThemeId(themeId);
            if (!result.IsSuccessful) return this.Error(result);

            var poems = result.Data.Select(this.ToThemeWithAuthorModel).ToList();

            return this.Ok(poems);

        }

        private PoemModel ToPoemModel(Poem poem)
        {
            return new PoemModel
            {
                Id = poem.Id,
                Name = poem.Name,
                TextUrl = poem.TextUrl,
                AnalysisUrl = poem.AnalysisUrl,
                ExtrasUrl = poem.ExtrasUrl
            };
        }

        private PoemWithAuthorModel ToThemeWithAuthorModel(Poem poem)
        {
            return new PoemWithAuthorModel
            {
                Id = poem.Id,
                Name = poem.Name,
                AuthorName = poem.Author.Name,
                AuthorPictureUrl = poem.Author.PictureUrl
            };
        }
    }
}
