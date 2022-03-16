﻿namespace Pishtova_ASP.NET_web_api.Model.Subject
{
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Collections.Generic;

    public class SubjectRankingByScores
    {
        public SubjectRankingByScores()
        {
            this.UsersPointsForSubject = new HashSet<UserPointsForSubjectModel>();
        }
        public ICollection<UserPointsForSubjectModel> UsersPointsForSubject { get; set; }
    }
}
