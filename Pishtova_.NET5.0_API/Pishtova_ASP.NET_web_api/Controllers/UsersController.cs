namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.User;

    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userservice)
        {
            this.userService = userservice;
        }

        [HttpGet]
        [Route(nameof(GetProfile))]
        public ActionResult<UserModel> GetProfile()
        {
            var userId = this.userService.GetUserId(User);
            return this.userService.GetProfileInfo(userId);

        }

    }
}
