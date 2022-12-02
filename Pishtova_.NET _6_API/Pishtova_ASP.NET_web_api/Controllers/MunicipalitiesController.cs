namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Municipality;

    public class MunicipalitiesController : ApiController
    {
        private readonly IMunicipalityService municipalityService;

        public MunicipalitiesController(IMunicipalityService municipalityService)
        {
            this.municipalityService = municipalityService ?? throw new System.ArgumentNullException(nameof(municipalityService));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var result = await municipalityService.GetAllAsync();
            if (!result.IsSuccessful) return this.Error(result);

            if (result.Data == null) return this.NotFound();
            var munisipalities = result.Data;

            var municipalyModels = munisipalities.Select(this.ToMunicipalityModel).ToList();

            return this.Ok(municipalyModels);
        }

        private MunicipalityModel ToMunicipalityModel(Municipality municipality)
        {
            return new MunicipalityModel { Id = municipality.Id, Name = municipality.Name };
        }
    }
}
