namespace Pishtova.Services.Data
{
    using System.Threading.Tasks;

    public interface ITownService
    {
        Task CreateAsync(string name);
    }
}
