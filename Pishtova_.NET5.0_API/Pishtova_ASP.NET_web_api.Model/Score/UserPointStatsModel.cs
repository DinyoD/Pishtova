using System.Collections.Generic;

namespace Pishtova_ASP.NET_web_api.Model.User
{
    public class UserPointStatsModel
    {
        public UserPointStatsModel()
        {
            this.Subjects = new HashSet<SubjectWithPointsByCategoryModel>();
        }
        public ICollection<SubjectWithPointsByCategoryModel> Subjects { get; set; }
    }
}
