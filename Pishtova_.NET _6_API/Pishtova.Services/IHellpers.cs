namespace Pishtova.Services
{
    using Pishtova.Services.Models;
    using System.Collections.Generic;

    public interface IHellpers
    {
        ICollection<SchoolDTO> ExtractAllSchoolsbyTownsAndMunicipality(string schoolsText);

        SubjectDTO Create_Bio_SubjectDTO(string firebaseCollectionName);

        SubjectDTO Create_Bg_SubjectDTO(string firebaseCollectionName);

    }
}
