namespace Pishtova.Services.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public class AuthorService : IAuthorService
    {
        private readonly PishtovaDbContext db;

        public AuthorService(PishtovaDbContext db)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }
    }
}
