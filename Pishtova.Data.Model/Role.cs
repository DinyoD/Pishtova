namespace Pishtova.Data.Model
{
    using System;

    using Microsoft.AspNetCore.Identity;

    using Pishtova.Common;
    using Pishtova.Data.Common.Model;

    public class Role : IdentityRole, IDeletableEntity, IAuditInfo
    {

        public Role()
            :this(null)
        {

        }

        public Role(string name = GlobalConstants.StudentRoleName)
            :base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }


        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }


        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
