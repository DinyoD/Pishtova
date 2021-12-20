namespace Pishtova.Services
{
    using System.Collections.Generic;

    using Pishtova.Services.Models;
    using ProjectHelper;
    using Sandbox;

    public class Helpers : IHellpers
    {
        public ICollection<SchoolDTO> ExtractAllSchoolsbyTownsAndMunicipality(string schoolInfoText)
        {
            var schoolsCollection = new List<SchoolDTO>();

            var collection = schoolInfoText.Split("},{");

            foreach (var item in collection)
            {
                if (!item.ToLower().Contains("детск") && !item.Contains("ДГ"))
                {
                    var schoolProps = item.Split(",");

                    var school = new SchoolDTO
                    {
                        TownDTO = new TownDTO
                        {
                            MunicipalityDTO = new MunicipalityDTO
                            {
                                Name = FixName(schoolProps[2]),
                            },
                            Name = FixName(schoolProps[3]),
                        },
                        Name = FixName(schoolProps[5]),
                    };

                    schoolsCollection.Add(school);
                }
            }

            return schoolsCollection;
        }

        public SubjectDTO ExtractSubjectProblems(string firebaseCollectionName)
        {
            var fbHelper = new FirebaseHelper(SandBoxConstants.AuthSecret, SandBoxConstants.BasePath);

            var subjectInfo = fbHelper.GetSubjectInfoFromFirebase(firebaseCollectionName);

            return createSubjectDTO(subjectInfo);
        }

        public SubjectDTO Extract_BG_SubjectProblems(string firebaseCollectionName)
        {
            var fbHelper = new FirebaseHelper(SandBoxConstants.AuthSecret, SandBoxConstants.BasePath);

            var subjectInfo = fbHelper.Get_BG_SubjectInfoFromFirebase(firebaseCollectionName);

            return create_BG_SubjectDTO(subjectInfo);
        }

        private SubjectDTO createSubjectDTO(List<List<List<string>>> subjectInfo)
        {
            throw new System.NotImplementedException();
        }

        private SubjectDTO create_BG_SubjectDTO(Dictionary<string, List<List<string>>> subjectInfo)
        {
            throw new System.NotImplementedException();
        }

        private static string FixName(string name)
        {
            var result = name.Trim('"');
            return result;
        }
    }
}
