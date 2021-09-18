namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiController
    {
        [Authorize]
        public ActionResult Get()
        {
            return Ok("work");
        }
    }
}
