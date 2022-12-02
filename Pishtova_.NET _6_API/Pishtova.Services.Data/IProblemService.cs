namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Pishtova.Data.Model;
    using Pishtova.Data.Common.Utilities;

    public interface IProblemService
    {
        Task<OperationResult<ICollection<Problem>>> GenerateTest(List<int> testPattern);
    }
}
