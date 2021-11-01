
namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Municipality;
    public class MunicipalitiesController : ApiController
    {
        private readonly IMunicipalityService municipalityService;

        public MunicipalitiesController(IMunicipalityService municipalityService)
        {
            this.municipalityService = municipalityService ?? throw new System.ArgumentNullException(nameof(municipalityService));
        }

        [HttpGet]
        [Route(nameof(All))]
        public async Task<ICollection<MunicipalityModel>> All()
        {
            var result =await municipalityService.GetAllAsync();
            return result;
        }
    }
}
