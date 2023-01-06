namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public  interface IThemeService
    {
        Task<OperationResult<ICollection<Theme>>> GetAllBySubjectId(string subjectId);
    }
}
