namespace Pishtova.Services.Data
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.WebUtilities;

    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova.Services.Messaging;
    using Pishtova_ASP.NET_web_api.Model.User;
    using Pishtova_ASP.NET_web_api.Model.School;

    public class UserService : IUserService
    {
        private readonly PishtovaDbContext db;
        private readonly IEmailSender emailSender;

        public UserService(
                PishtovaDbContext db,
                IEmailSender emailSender)
        { 
            this.db = db;
            this.emailSender = emailSender;
        }

        public UserModel GetProfileInfo(string userId)
        {
            var profile =  this.db.Users
                .Where(x => x.Id == userId)
                .Include(x => x.UserScores).ThenInclude(x => x.SubjectCategory).ThenInclude(x => x.Subject)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.Name,
                    PictureUrl = u.PictureUrl,
                    Grade = u.Grade,
                    TownName = u.School.Town.Name,
                    School = new SchoolForUserModel
                    { 
                        Name = u.School.Name,
                        Id = u.School.Id
                    },
                    Stats = getStatsByScores(u.UserScores),
                })
                .FirstOrDefault();
            return profile;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }

        public async Task UpdateUserAvatar(string userId, string pictureUrl)
        {
            var dbUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
            dbUser.PictureUrl = pictureUrl;
            await this.db.SaveChangesAsync();
        }

        public async Task<User> UpdateUserInfo(string userId, UserUpdateInfoModel model)
        {
            var dbUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));

            this.db.Update(dbUser);
            dbUser.Name = model.Name;
            dbUser.Grade = model.Grade;
            dbUser.SchoolId = model.SchoolId;
            await this.db.SaveChangesAsync();

            return dbUser;
        }

        public async Task<User> UpdateUserEmail(string userId, UserChangeEmailModel model)
        {
            var dbUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
            if (dbUser.NormalizedEmail != model.Email.ToUpper())
            {
                this.db.Update(dbUser);
                dbUser.Email = model.Email;
                dbUser.NormalizedEmail = model.Email.ToUpper();
                dbUser.UserName = model.Email;
                dbUser.NormalizedUserName = model.Email.ToUpper();
                dbUser.EmailConfirmed = false;
                await this.db.SaveChangesAsync();
            }
            return dbUser;
        }

        public async Task SendEmailConfirmationTokenAsync(string clientURI, string email, string token)
        {
            //var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string>
                            {
                                {"token", token },
                                {"email", email }
                            };
            var callback = QueryHelpers.AddQueryString(clientURI, param);
            var message = new Message(new string[] { email }, "Email Confirmation token", callback, null);
            await this.emailSender.SendEmailAsync(message);
        }

        public async Task SendResetPaswordTokenAsync(string clientURI, string email, string token)
        {
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", email }
            };

            var callback = QueryHelpers.AddQueryString(clientURI, param);

            var message = new Message(new string[] { email }, "Reset password token", callback, null);
            await emailSender.SendEmailAsync(message);
        }

        private static UserPointStatsModel getStatsByScores(ICollection<Score> userScores)
        {
            var result = new UserPointStatsModel();
            foreach (var item in userScores)
            {
                var subject = result.Subjects.FirstOrDefault(x => x.SubjectName == item.SubjectCategory.Subject.Name);

                if (subject != null)
                {
                    var category = result.Subjects
                        .Where(x => x.SubjectName == item.SubjectCategory.Subject.Name)
                        .First().SubjectCategories
                        .FirstOrDefault(x => x.CategoryName == item.SubjectCategory.Name);

                    if (category != null)
                    {
                        var currCategory = result.Subjects
                            .Where(x => x.SubjectName == item.SubjectCategory.Subject.Name)
                            .First().SubjectCategories
                            .Where(x => x.CategoryName == item.SubjectCategory.Name)
                            .First();

                        currCategory.Points += item.Points;
                        currCategory.ProblemsCount += 1;
                    }
                    else
                    {
                        var currSubject = result.Subjects
                            .Where(x => x.SubjectName == item.SubjectCategory.Subject.Name)
                            .First();

                        currSubject.SubjectCategories.Add(new CategoryWithPointsModel {
                                                                    CategoryName = item.SubjectCategory.Name,
                                                                    Points = item.Points,
                                                                    ProblemsCount = 1
                                                                });
                    }
                }
                else
                {
                    var newSubject = new SubjectWithPointsByCategoryModel
                    {
                        SubjectName = item.SubjectCategory.Subject.Name
                    };
                    newSubject.SubjectCategories.Add(new CategoryWithPointsModel {
                                                            CategoryName = item.SubjectCategory.Name,
                                                            Points = item.Points,
                                                            ProblemsCount = 1
                                                        });
                    result.Subjects.Add(newSubject);
                }
            }

            return result;
        }
    }
}
