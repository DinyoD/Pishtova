namespace Pishtova.Services.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Pishtova.Data.Common.Utilities;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.User;

    public interface IUserService
    {
        // TODO Remove method
        Task<string> GetUserIdAsync(ClaimsPrincipal user);

        Task<OperationResult<User>> GetByIdAsync(string userId);

        Task<OperationResult> UpdateUserAvatar(UserToUpdatePictireUrlModel model);

        Task<OperationResult> UpdateUserInfo(UserToUpdateModel model);

        Task<OperationResult<User>> UpdateUserEmail(UserToUpdateEmailModel model);
        
        Task<OperationResult> SendEmailConfirmationTokenAsync(string clientURI, string email, string token);

        Task<OperationResult> SendResetPaswordTokenAsync(string clientURI, string email, string token);

        Task<OperationResult<User>> DeleteByIdAsync(string id);
    }
}
