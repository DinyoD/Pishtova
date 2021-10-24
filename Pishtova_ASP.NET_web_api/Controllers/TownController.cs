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
    }
}
