namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    public interface ISchoolService
    {
        Task CreateAsync(string name, int townId);
    }

}
