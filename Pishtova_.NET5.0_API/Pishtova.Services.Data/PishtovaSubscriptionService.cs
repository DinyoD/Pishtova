using Microsoft.EntityFrameworkCore;
using Pishtova.Data;
using Pishtova.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pishtova.Services.Data
{
    public class PishtovaSubscriptionService: IPishtovaSubscriptionService
    {
        private readonly PishtovaDbContext db;

        public PishtovaSubscriptionService(PishtovaDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

		public async Task<Subscriber> CreateAsync(Subscriber subscription)
		{
			await this.db.Subscribers.AddAsync(subscription);
			await this.db.SaveChangesAsync();
			return subscription;
		}

		public async Task DeleteAsync(Subscriber subscription)
		{
			this.db.Subscribers.Remove(subscription);
			await this.db.SaveChangesAsync();
		}

		public async Task<ICollection<Subscriber>> GetAsync()
		{
			return await this.db.Subscribers.ToListAsync();
		}

		public async Task<Subscriber> GetByCustomerIdAsync(string id)
		{
			return await this.db.Subscribers.SingleOrDefaultAsync(x => x.CustomerId == id);
		}

		public async Task<Subscriber> GetByIdAsync(string id)
		{
			return await this.db.Subscribers.SingleOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Subscriber> UpdateAsync(Subscriber subscription)
		{
			this.db.Subscribers.UpdateRange(subscription);
			await this.db.SaveChangesAsync();
			return subscription;
		}
    }
}
