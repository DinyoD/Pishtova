namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Security.Claims;

    public class UsersController : ApiController
    {
        private readonly IUserService userservice;
        private readonly UserManager<User> userManager;

        public UsersController(
            IUserService userservice,
            UserManager<User> userManager)
        {
            this.userservice = userservice;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route(nameof(GetProfile))]
        public ActionResult<UserModel> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.Email);
            return this.userservice.GetProfileInfo(userId);

        }
    }
}
