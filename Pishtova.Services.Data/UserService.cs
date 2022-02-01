namespace Pishtova.Services.Data
{
    using Pishtova.Data;
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly PishtovaDbContext db;

        public UserService(PishtovaDbContext db)
        {
            this.db = db;
        }

        public UserModel GetProfileInfo(string userId)
        {
            return this.db.Users
                .Where(x => x.Id == userId)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    PictureUrl = u.PictureUrl,
                    Grade = u.Grade,
                    TownName = u.School.Town.Name,
                    SchoolName = u.School.Name,
                    UserScores = u.UserScores
                        .Select(s => new UserScoreModel { 
                            Points = s.Points,
                            SubjectCategory = new SubjectCategoryModel
                            {
                                Id = s.SubjectCategory.Id,
                                Name = s.SubjectCategory.Name,
                                Subject = new SubjectModel {
                                    Id = s.SubjectCategory.Subject.Id,
                                    Name = s.SubjectCategory.Subject.Name
                                }
                            }
                        })
                        .ToList(),
                })
                .FirstOrDefault();
        }
    }
}
