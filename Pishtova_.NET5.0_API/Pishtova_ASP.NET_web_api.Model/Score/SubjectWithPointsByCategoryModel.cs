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

        public ICollection<CategoryWithPointsModel> SubjectCategories { get; set; }
    }
}