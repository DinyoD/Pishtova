namespace Pishtova.Services.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.User;

    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal user);

        UserProfileDTO GetProfileInfo(string userId);

        Task UpdateUserAvatar(string userId, string pictureUrl);

        Task UpdateUserInfo(string userId, UserInfoDTO model);

        Task<User> UpdateUserEmail(string userId, UserChangeEmailDTO model);

        Task SendEmailConfirmationTokenAsync(string clientURI, string email, string token);

        Task SendResetPaswordTokenAsync(string clientURI, string email, string token);
    }
}
