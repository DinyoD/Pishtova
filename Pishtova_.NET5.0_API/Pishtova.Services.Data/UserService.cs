namespace Pishtova.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly PishtovaDbContext db;

        public UserService(PishtovaDbContext db)
        {
            this.db = db;
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
                    SchoolName = u.School.Name,
                    Stats = getStatsByScores(u.UserScores),
                })
                .FirstOrDefault();
            return profile;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
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

        public async Task UpdateUserAvatar(string userId, string pictureUrl)
        {
            var dbUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
            dbUser.PictureUrl = pictureUrl;
            await this.db.SaveChangesAsync();
        }
    }
}
