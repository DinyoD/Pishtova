namespace Pishtova.Services
{
    using Pishtova.Services.Models;
    using System.Collections.Generic;
    public interface IHellpers
    {
        ICollection<SchoolsDTO> ExtrackAllSchoolsbyTownsAndMunicipality(string schoolsText);
    }
}
