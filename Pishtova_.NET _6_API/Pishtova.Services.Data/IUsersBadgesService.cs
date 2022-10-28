namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface IUsersBadgesService
    {
        /// <summary>
        /// Asynchronously find an entity(UserBadge) with the given primary key(Id) value.
        /// </summary>
        /// <param name="id">The values of the primary key(Id) for the entity to be found.</param>
        /// <returns>A task whose result contains OperationResult object with Data property - founded entity(UserBadge). If no entity is found, then
        ///     Data is null</returns>
        Task<OperationResult<UserBadge>> GetByIdAsync(int id);
        /// <summary>
        /// Asynchronously save entity(UserBadge) in Db.
        /// </summary>
        /// <param name="userBadge"></param>
        /// <returns>A task whose result contains OperationResult object with Data property - primary key(Id) value of the newly created userBadge.</returns>
        Task<OperationResult<int>> CreateAsync(UserBadge userBadge);
        /// <summary>
        /// Asynchronously find all entities(UserBadges) with the given userId value.
        /// </summary>
        /// <param name="id">The values of the userId for the entities to be found.</param>
        /// <returns>A task whose result contains OperationResult object with Data property - collection of entities(UserBadges) with the given userId. If no entity is found, then
        ///     Data is empty collection.</returns>
        Task<OperationResult<ICollection<UserBadge>>> GetAllByUserAsync(string userId);
        /// <summary>
        /// Asynchronously find all entities(UserBadges) with the given testId value.
        /// </summary>
        /// <param name="id">The values of the testId for the entities to be found.</param>
        /// <returns>A task whose result contains OperationResult object with Data property - collection of entities(UserBadges) with the given testId. If no entity is found, then
        ///     Data is empty collection.</returns>
        Task<OperationResult<ICollection<UserBadge>>> GetAllByTestAsync(int testId);
    }

}
