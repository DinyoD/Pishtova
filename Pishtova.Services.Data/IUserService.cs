namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Threading.Tasks;

    public interface IUserService
    {
        UserModel GetProfileInfo(string userId);
    }
}
