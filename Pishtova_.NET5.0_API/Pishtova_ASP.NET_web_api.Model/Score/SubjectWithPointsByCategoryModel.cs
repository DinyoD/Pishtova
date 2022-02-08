using System.Collections.Generic;
using System.Linq;

namespace Pishtova_ASP.NET_web_api.Model.User
{
    public class SubjectWithPointsByCategoryModel
    {
        public SubjectWithPointsByCategoryModel()
        {
            this.SubjectCategories = new HashSet<CategoryWithPointsModel>();
        }
        public string SubjectName { get; set; }

        public ICollection<CategoryWithPointsModel> SubjectCategories { get; set; }

        public int SubjectAllPoints => this.SubjectCategories.Sum(x => x.Points);

        public int SubjectAllProblems => this.SubjectCategories.Sum(x => x.ProblemsCount);
    }
}