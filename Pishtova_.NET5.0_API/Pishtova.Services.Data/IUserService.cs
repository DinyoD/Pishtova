namespace Pishtova.Services.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.User;

    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal user);

        UserModel GetProfileInfo(string userId);

        Task UpdateUserAvatar(string userId, string pictureUrl);

        Task<User> UpdateUserInfo(string userId, UserUpdateInfoModel model);

        Task<User> UpdateUserEmail(string userId, UserChangeEmailModel model);

        Task SendEmailConfirmationTokenAsync(string clientURI, string email, string token);

        Task SendResetPaswordTokenAsync(string clientURI, string email, string token);
    }
}
