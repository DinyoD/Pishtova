namespace Pishtova.Services.Data
{
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using Pishtova.Data.Model;

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
