namespace Pishtova.Services
{
    using Pishtova.Services.Models;
    using System.Collections.Generic;

    public interface IHelpers
    {
        ICollection<SchoolDTO> ExtractAllSchoolsbyTownsAndMunicipality(string schoolsText);

        SubjectDTO Create_Bio_SubjectDTO(string firebaseCollectionName, string subjectName, string subjectId);

        SubjectDTO Create_Bg_SubjectDTO(string firebaseCollectionName, string subjectName, string subjectId);

        SubjectDTO CreateSubjectDTO(string firebaseCollectionName, string subjectName, string subjectId);

    }
}
