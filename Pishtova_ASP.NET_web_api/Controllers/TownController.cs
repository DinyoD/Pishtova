namespace Pishtova_ASP.NET_web_api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Town;

    public class TownController : ApiController
    {
        private readonly ITownService townService;

        public TownController(ITownService townService)
        {
            this.townService = townService;
        }

        [Route(nameof(Add))]
        [HttpPost]
        public async Task<ActionResult<string>> Add(TownAddModel model)
        {
            try
            {
                await this.townService.CreateAsync(model.Name);
                return Ok();
            }
            catch (Exception er)
            {
                return BadRequest(er.Message);
            }

        }
    }
}
