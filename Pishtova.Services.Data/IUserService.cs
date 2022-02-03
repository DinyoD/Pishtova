﻿namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Security.Claims;

    public interface IUserService
    {
        UserModel GetProfileInfo(string userId);

        string GetUserId(ClaimsPrincipal user);
    }
}
