namespace Pishtova.Services
{
    using System.Collections.Generic;

    using Pishtova.Services.Models;

    public class Helpers : IHellpers
    {
        public ICollection<SchoolsDTO> ExtrackAllSchoolsbyTownsAndMunicipality(string schoolInfoText)
        {
            var schoolsCollection = new List<SchoolsDTO>();

            var collection = schoolInfoText.Split("},{");

            foreach (var item in collection)
            {
                if (!item.ToLower().Contains("детск") && !item.Contains("ДГ"))
                {
                    var schoolProps = item.Split(",");

                    var school = new SchoolsDTO
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

        public string FixName(string name)
        {
            var result = name.Trim('"');
            return result;
        }
    }
}
