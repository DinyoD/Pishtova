namespace Pishtova.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Pishtova.Data.Common.Model;

    public class Badge : BaseDeletableModel<string>
    {
        public Badge()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserBadges = new HashSet<UserBadge>();
        }

        [Required]
        public string PictureUrl { get; set; }

        public virtual ICollection<UserBadge> UserBadges { get; set; }
    }
}
