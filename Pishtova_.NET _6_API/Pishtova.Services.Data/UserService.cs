namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.WebUtilities;

    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova.Services.Messaging;
    using Pishtova.Data.Common.Utilities;
    using Pishtova_ASP.NET_web_api.Model.User;

    public class UserService : IUserService
    {
        private readonly PishtovaDbContext db;
        private readonly IEmailSender emailSender;
        private readonly UserManager<User> userManager;

        public UserService(
            PishtovaDbContext db,
            IEmailSender emailSender,
            UserManager<User> userManager)
        { 
            this.db = db;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        public async Task<OperationResult<User>> GetByIdAsync(string userId)
        {
            var operationResult = new OperationResult<User>();
            if (!operationResult.ValidateNotNull(userId)) return operationResult;

            try
            {
                var user = await this.db.Users
                    .Where(x => x.Id == userId)                    
                    .Include(x => x.School)
                    .ThenInclude(sc => sc.Town)
                    .FirstOrDefaultAsync();

                operationResult.Data = user;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult> UpdateUserAvatar(UserToUpdatePictireUrlModel model)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(model)) return operationResult;

            try
            {
                var dbUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
                dbUser.PictureUrl = model.PictureUrl;
                await this.db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }
            return operationResult;
        }

        public async Task<OperationResult> UpdateUserInfo(UserToUpdateModel model)
        {
            var operationResult = new OperationResult();
            if (!operationResult.ValidateNotNull(model)) return operationResult;

            try
            {
                var dbUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
                dbUser.Name = model.Name;
                dbUser.Grade = model.Grade;
                dbUser.SchoolId = model.SchoolId;
                await this.db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }

            return operationResult;           
        }

        public async Task<OperationResult<User>> UpdateUserEmail(UserToUpdateEmailModel model)
        {
            var operationResult = new OperationResult<User>();
            if (!operationResult.ValidateNotNull(model)) return operationResult;

            try
            {
                var dbUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (dbUser.NormalizedEmail == model.Email.ToUpper()) operationResult.AddError( new Error() { Message = "Email is the same" });
                if (!operationResult.IsSuccessful) return operationResult;

                dbUser.Email = model.Email;
                dbUser.NormalizedEmail = model.Email.ToUpper();
                dbUser.UserName = model.Email;
                dbUser.NormalizedUserName = model.Email.ToUpper();
                dbUser.EmailConfirmed = false;
                await this.db.SaveChangesAsync();

                operationResult.Data = dbUser;
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }

            return operationResult;
        }

        public async Task<OperationResult> SendEmailConfirmationTokenAsync(string clientURI, string email, string token)
        {
            var operationResult = new OperationResult<User>();
            if (!operationResult.ValidateNotNull(clientURI)) return operationResult;
            if (!operationResult.ValidateNotNull(email)) return operationResult;
            if (!operationResult.ValidateNotNull(token)) return operationResult;

            try
            {
                var param = new Dictionary<string, string>
                                {
                                    {"token", token },
                                    {"email", email }
                                };
                var callback = QueryHelpers.AddQueryString(clientURI, param);
                var message = new Message(new string[] { email }, "Email Confirmation token", callback, null);
                await this.emailSender.SendEmailAsync(message);

            }
            catch (Exception e)
            {
                operationResult.AddException(e);    
            }

            return operationResult;
        }

        public async Task<OperationResult> SendResetPaswordTokenAsync(string clientURI, string email, string token)
        {
            var operationResult = new OperationResult<User>();
            if (!operationResult.ValidateNotNull(clientURI)) return operationResult;
            if (!operationResult.ValidateNotNull(email)) return operationResult;
            if (!operationResult.ValidateNotNull(token)) return operationResult;

            try
            {
                var param = new Dictionary<string, string>
                {
                    {"token", token },
                    {"email", email }
                };

                var callback = QueryHelpers.AddQueryString(clientURI, param);

                var message = new Message(new string[] { email }, "Reset password token", callback, null);
                await this.emailSender.SendEmailAsync(message);
            }
            catch (Exception e)
            {
                operationResult.AddException(e);
            }

            return operationResult;
        }


        // TODO Remove method
        public async Task<string> GetUserIdAsync(ClaimsPrincipal principal)
        {
            var userEmail = principal.FindFirstValue(ClaimTypes.Email);
            var user = await this.userManager.FindByNameAsync(userEmail);
            return user.Id;
        }
    }
}
