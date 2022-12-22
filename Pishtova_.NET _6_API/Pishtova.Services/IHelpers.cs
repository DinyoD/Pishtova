namespace Pishtova.Services
{
    using Pishtova.Services.Models;
    using System.Collections.Generic;

    public interface IHelpers
    {
        ICollection<SchoolDTO> ExtractAllSchoolsbyTownsAndMunicipality(string schoolsText);

        SubjectDTO Create_BioFromFB_SubjectDTO(string firebaseCollectionName, string subjectName, string subjectId);

        SubjectDTO Create_Bg12FromFB_SubjectDTO(string firebaseCollectionName, string subjectName, string subjectId);

        SubjectCategoryDTO Create_FromFile_CategoryDTO(string fileName);

        SubjectDTO Create_FromFile_SubjectDTO(string firebaseCollectionName, string subjectName, string subjectId);

    }
}
