namespace Pishtova.Services
{
    using Pishtova.Services.Models;
    using System.Collections.Generic;
    public interface IHellpers
    {
        ICollection<SchoolDTO> ExtractAllSchoolsbyTownsAndMunicipality(string schoolsText);

        SubjectDTO Extract_BG_SubjectProblems(string firebaseCollectionName);
    }
}
