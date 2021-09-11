namespace Pishtova.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;

    using Pishtova.Data.Common.Model;

    public class User : IdentityUser<string>, IDeletableEntity, IAuditInfo
    {

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            //this.Roles = new HashSet<IdentityUserRole<string>>();

            this.UserScores = new HashSet<Score>();
            this.BadgeUsers = new HashSet<UserBadge>();
            this.UserGroups = new HashSet<UserGroup>();
        }

        public string PictureUrl { get; set; }

        [Range(4, 12)]
        public int Grade { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public virtual ICollection<Score> UserScores { get; set; }

        public virtual ICollection<UserBadge> BadgeUsers { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }

        //[NotMapped]
        //public int TotalScores => this.UserScores.Sum(x => x.Points); 

        //public Sex Sex { get; set; }

        //Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }


        //Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        //public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

    }
}
