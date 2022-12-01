namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.Town;

    public class TownsController : ApiController
    {
        private readonly ITownService townService;

        public TownsController(ITownService townService)
        {
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
        }

        [HttpGet]
        [Route("municipality/{municipalityId}")]
        public async Task<IActionResult> GetAllByMunicipalityId([FromRoute]int municipalityId)
        {
            var result = await this.townService.GetAllByMunicipalityId(municipalityId);
            if (!result.IsSuccessful) return this.Error(result);

            if (result.Data == null) return this.NotFound();   
            var townModels = result.Data.Select(this.ToTownModel).ToList();

            return Ok(townModels);
        }

        private TownModel ToTownModel(Town dbTowm)
        {
            return new TownModel
            {
                Id = dbTowm.Id,
                Name = dbTowm.Name,
                MunicipalityId = dbTowm.MunicipalityId
            };
        }
    }
}
