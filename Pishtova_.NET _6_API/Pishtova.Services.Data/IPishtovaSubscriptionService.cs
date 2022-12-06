namespace Pishtova.Services.Data
{
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using Pishtova.Data.Model;

    public interface IPishtovaSubscriptionService
    {
		Task<Subsription> UpdateAsync(Subsription subscription);

		Task<ICollection<Subsription>> GetAsync();

		Task<Subsription> GetByIdAsync(string id);

		Task<Subsription> GetByCustomerIdAsync(string id);

		Task<Subsription> CreateAsync(Subsription subscription);

		Task DeleteAsync(Subsription subscription);
	}
}
