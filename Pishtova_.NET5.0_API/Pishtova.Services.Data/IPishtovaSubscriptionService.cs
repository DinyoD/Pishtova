﻿using Pishtova.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pishtova.Services.Data
{
    public interface IPishtovaSubscriptionService
    {
		Task<Subscriber> UpdateAsync(Subscriber subscription);
		Task<ICollection<Subscriber>> GetAsync();
		Task<Subscriber> GetByIdAsync(string id);
		Task<Subscriber> GetByCustomerIdAsync(string id);
		Task<Subscriber> CreateAsync(Subscriber subscription);
		Task DeleteAsync(Subscriber subscription);
	}
}
