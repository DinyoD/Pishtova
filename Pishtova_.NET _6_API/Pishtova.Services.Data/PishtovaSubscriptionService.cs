﻿namespace Pishtova.Services.Data
{
	using Microsoft.EntityFrameworkCore;
	using Pishtova.Data;
	using Pishtova.Data.Common.Utilities;
	using Pishtova.Data.Model;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

    public class PishtovaSubscriptionService: IPishtovaSubscriptionService
    {
        private readonly PishtovaDbContext db;

        public PishtovaSubscriptionService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

		public async Task<Subsription> CreateAsync(Subsription subscription)
		{
			await this.db.Subsriptions.AddAsync(subscription);
			await this.db.SaveChangesAsync();
			return subscription;
		}

		public async Task DeleteAsync(Subsription subscription)
		{
			this.db.Subsriptions.Remove(subscription);
			await this.db.SaveChangesAsync();
		}

		public async Task<ICollection<Subsription>> GetAsync()
		{
			return await this.db.Subsriptions.ToListAsync();
		}

		public async Task<OperationResult<Subsription>> GetByCustomerIdAsync(string id)
		{
            var result = new OperationResult<Subsription>();
            if (!result.ValidateNotNull(id)) return result;

            try
            {
                var subcr = await this.db.Subsriptions.FirstOrDefaultAsync(x => x.CustomerId == id);
                return result.WithData(subcr);
            }
            catch (Exception e)
            {
                result.AddException(e);
                return result;
            }
        }

		public async Task<Subsription> GetByIdAsync(string id)
		{
			return await this.db.Subsriptions.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Subsription> UpdateAsync(Subsription subscription)
		{
			this.db.Subsriptions.UpdateRange(subscription);
			await this.db.SaveChangesAsync();
			return subscription;
		}
    }
}
