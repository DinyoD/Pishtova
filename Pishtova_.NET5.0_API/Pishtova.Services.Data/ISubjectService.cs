namespace Pishtova.Services.Data
{
    using Pishtova_ASP.NET_web_api.Model.Subject;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubjectService
    {
        Task<ICollection<SubjectDTO>> GetAll();

        Task<SubjectDTO> GetOneById(int id);
    }
}
