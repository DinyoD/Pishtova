namespace Pishtova.Services.Data
{
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal user);

        UserModel GetProfileInfo(string userId);

        Task UpdateUserAvatar(string userId, string pictureUrl);
    }
}
