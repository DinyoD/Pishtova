namespace Pishtova.Data.Model
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using Pishtova.Data.Common.Model;

    public class User : IdentityUser<string>, IDeletableEntity, IAuditInfo
    {

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
        }


        //Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }


        //Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

    }
}
