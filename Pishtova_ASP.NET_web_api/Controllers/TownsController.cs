namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Town;

    public class TownsController : ApiController
    {
        private readonly ITownService townService;

        public TownsController(ITownService townService)
        {
            this.townService = townService ?? throw new ArgumentNullException(nameof(townService));
        }

        [HttpGet]
        [Route(nameof(byMunicipality))]
        public async Task<ICollection<TownModel>> byMunicipality(int municipalityId)
        {
            return await this.townService.GetAllByMunicipalityId(municipalityId);
        }
    }
}
