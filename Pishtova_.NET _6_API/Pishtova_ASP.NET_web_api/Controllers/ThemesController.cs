namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Theme;

    public class ThemesController: ApiController
    {
        private readonly IThemeService themeService;

        public ThemesController(IThemeService themeService)
        {
            this.themeService = themeService ?? throw new ArgumentNullException(nameof(themeService));
        }

        [HttpGet]
        [Route("subject/{subjectId}")]
        public async Task<IActionResult> getThemesBySubjectId([FromRoute]string subjectId)
        {
            var result = await this.themeService.GetAllBySubjectId(subjectId);
            if (!result.IsSuccessful) return this.Error(result);

            var themes = result.Data.Select(this.ToThemeModel).ToList();

            return this.Ok(themes);

        }

        private ThemeModel ToThemeModel(Theme theme)
        {
            return new ThemeModel
            {
                Id = theme.Id,
                Name = theme.Name
            };
        }
    }
}
