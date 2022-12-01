
namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Extensions;
    using Pishtova_ASP.NET_web_api.Model.School;

    public class SchoolsController : ApiController
    {
        private readonly ISchoolService schoolService;

        public SchoolsController( ISchoolService schoolService)
        {
            this.schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));
        }

        [HttpGet]
        [Route("town/{townId}")]
        public async Task<IActionResult> GatAllByTownId([FromRoute]int townId)
        {
            var result = await this.schoolService.GetAllByTownIdAsync(townId);
            if (!result.IsSuccessful) return this.Error(result);

            if (result.Data == null) return this.NotFound();
            var schools = result.Data;
            var schoolsModels = schools.Select(this.ToSchoolModel).ToList();

            return this.Ok(schoolsModels);
        }

        private SchoolModel ToSchoolModel(School school)
        {
            return new SchoolModel 
            {
                Id= school.Id,
                Name= school.Name,
                TownId= school.TownId,
            };

        }
    }
}
