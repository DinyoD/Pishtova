﻿namespace Pishtova.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using Pishtova.Data.Common.Model;

    public class User : IdentityUser<string>, IDeletableEntity, IAuditInfo
    {

        public User()
        {
            this.Id = Guid.NewGuid().ToString();

            this.UserScores = new HashSet<Score>();
            this.UserBadges = new HashSet<UserBadge>();
            this.UserGroups = new HashSet<UserGroup>();
            this.UserTest = new HashSet<Test>();
        }

        [Required]
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        [Range(4, 12)]
        public int Grade { get; set; }

        [Required]
        public int SchoolId { get; set; }

        public School School { get; set; }

        public string CustomerId { get; set; }

        public virtual ICollection<Score> UserScores { get; set; }

        public virtual ICollection<UserBadge> UserBadges { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public virtual ICollection<Test> UserTest { get; set; }

        //Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        //Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }
}
