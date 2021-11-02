
namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.School;

    public class SchoolController : ApiController
    {
        private readonly ISchoolService schoolService;

        public SchoolController( ISchoolService schoolService)
        {
            this.schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));
        }

        [HttpGet]
        [Route("bytown/{townId}")]
        public async Task<ICollection<SchoolModel>> ByTownId(int townId)
        {
            if (townId == 0) throw new ArgumentNullException(nameof(townId));

            return await this.schoolService.GetAllByTownId(townId);
        }
    }
}
