﻿using System.Collections.Generic;
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
    }
}