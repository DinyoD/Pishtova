namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface IBadgeService
    {
        /// <summary>
        /// Asynchronously find the entity(Badge) with the given code value.
        /// </summary>
        /// <param name="code">The values of the code property for the entity to be found.</param>
        /// <returns>A task whose result contains OperationResult object with Data property - founded entity(Badge). If no entity is found, then
        ///     Data is null.</returns>
        Task<OperationResult<Badge>> GetByCodeAsync(int code);
    }
}
