namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface IPoemService
    {
        Task<OperationResult<Poem>> GetById(string id);

        Task<OperationResult<ICollection<Poem>>> GetAllByThemeId(string themeId);
    }
}
