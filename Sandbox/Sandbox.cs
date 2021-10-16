
namespace Sandbox
{
    using Pishtova.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Sandbox
    {
        public  ICollection<School> schools = new List<School>();

        string schoolString = File.ReadAllText(@"C:\Users\Dinyo\Desktop\Pishtova-docs\schools-BG-15.10.18.json");

        public  ICollection<School> GetSchool()
        {
            var collection = schoolString.Split("},{");

            foreach (var item in collection)
            {
                if (!item.ToLower().Contains("детск"))
                {
                    var beautifiedItem = item.Replace("\"", "").Replace("\\", "\"");
                    var schoolProps = beautifiedItem.Split(",");



                    var school = new School {
                        Town = new Town {
                            Name = schoolProps[3],
                        },
                        Name = schoolProps[5],
                    };

                    this.schools.Add(school);
                }
            }

            return this.schools;
        }

    }
}
