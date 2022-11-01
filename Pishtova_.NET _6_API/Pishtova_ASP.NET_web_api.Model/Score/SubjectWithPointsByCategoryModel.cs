using System.Collections.Generic;
using Pishtova_ASP.NET_web_api.Model.Category;

namespace Pishtova_ASP.NET_web_api.Model.User
{
    public class SubjectWithPointsByCategoryModel
    {
        public SubjectWithPointsByCategoryModel()
        {
            this.SubjectCategories = new HashSet<CategoryScoreModel>();
        }

        public ICollection<CategoryScoreModel> SubjectCategories { get; set; }
    }
}