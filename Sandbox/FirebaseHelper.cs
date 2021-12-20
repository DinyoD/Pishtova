using System.Collections.Generic;

namespace Sandbox
{
    using Newtonsoft.Json;
    using FireSharp.Config;
    using FireSharp.Response;
    using FireSharp.Interfaces;
    using FireSharp;
    using System.Collections.Generic;
    using System.Linq;

    public class FirebaseHelper
    {
        private readonly IFirebaseConfig config;
        private readonly IFirebaseClient client;

        public FirebaseHelper(string authSecret, string basePath)
        {
            this.config = new FirebaseConfig()
            {
                AuthSecret = authSecret,
                BasePath = basePath
            };
            this.client = new FirebaseClient(this.config);
        }

        // Geo-Eng-Bio
        //public List<List<List<string>>> RetriveDbColection(string colectionName)
        //{
        //    FirebaseResponse fbResponce = this.client.Get(colectionName);
        //    var result = JsonConvert.DeserializeObject<List<List<List<string>>>>(fbResponce.Body.ToString());
        //    return result;
        //}        
        //public Subject GetSubjectInfo(string collectionName)
        //{
        //    var subjectInfo = RetriveDbColection(collectionName);
        //    var subjectName = collectionName.Split('_')[1];

        //    return new Subject
        //    {
        //        Name = subjectName,
        //        Categories = ExtractInfo(subjectInfo)
        //    };
        //}
        //public ICollection<Category> ExtractInfo(List<List<List<string>>> subjectInfo)
        //{
        //    ICollection<Category> categories = new List<Category>();
        //    for (int i = 0; i < subjectInfo.Count; i++)
        //    {
        //        var categoryInfo = subjectInfo[i];
        //        if (categoryInfo == null)
        //        {
        //            continue;
        //        }
        //        ICollection<Problem> problems = new List<Problem>();
        //        for (int j = 0; j < categoryInfo.Count; j++)
        //        {
        //            var problemInfo = categoryInfo[j];
        //            if (problemInfo == null)
        //            {
        //                continue;
        //            }
        //            var problem = new Problem
        //            {
        //                QuestionText = problemInfo[0],
        //                Answers = new List<Answer>{
        //                    new Answer { Text = problemInfo[1], IsCorrect = problemInfo[1] == problemInfo[5] },
        //                    new Answer { Text = problemInfo[2], IsCorrect = problemInfo[2] == problemInfo[5] },
        //                    new Answer { Text = problemInfo[3], IsCorrect = problemInfo[3] == problemInfo[5] },
        //                    new Answer { Text = problemInfo[4], IsCorrect = problemInfo[4] == problemInfo[5] }
        //                },
        //                Hint = problemInfo.Count > 6 && !string.IsNullOrWhiteSpace(problemInfo[6]) ? problemInfo[6] : null,
        //                PictureUrl = problemInfo.Count > 7 && !string.IsNullOrWhiteSpace(problemInfo[7]) ? problemInfo[7] : null,
        //            };
        //            problems.Add(problem);
        //        }
        //        var category = new Category
        //        {
        //            CategoryName = i,
        //            Problems = problems
        //        };
        //        categories.Add(category);
        //    }
        //    return categories;
        //}


        // Bulgarian

        public Dictionary<string,List<List<string>>> RetriveDbColection(string colectionName)
        {
            FirebaseResponse fbResponce = this.client.Get(colectionName);
            var responce = fbResponce.Body.ToString().Split(",\"analiz")[0] +"}";
            var result = JsonConvert.DeserializeObject<Dictionary<string,List<List<string>>>> (responce);
            return result;
        }
        public Subject GetSubjectInfo(string collectionName)
        {
            var subjectInfo = RetriveDbColection(collectionName);
            var subjectName = collectionName.Split('_')[1];

            return new Subject
            {
                Name = subjectName,
                Categories = ExtractInfo(subjectInfo)
            };
        }
        public ICollection<Category> ExtractInfo(Dictionary<string, List<List<string>>> subjectInfo)
        {
            ICollection<Category> categories = new List<Category>();
            foreach (var kvp in subjectInfo)
            {
                var categoryInfo = kvp.Value;
                if (categoryInfo == null)
                {
                    continue;
                }
                ICollection<Problem> problems = new List<Problem>();
                for (int j = 0; j < categoryInfo.Count; j++)
                {
                    var problemInfo = categoryInfo[j];
                    if (problemInfo == null)
                    {
                        continue;
                    }
                    var problem = new Problem
                    {
                        QuestionText = problemInfo[0],
                        Answers = new List<Answer>{
                            new Answer { Text = problemInfo[1], IsCorrect = problemInfo[1] == problemInfo[5] },
                            new Answer { Text = problemInfo[2], IsCorrect = problemInfo[2] == problemInfo[5] },
                            new Answer { Text = problemInfo[3], IsCorrect = problemInfo[3] == problemInfo[5] },
                            new Answer { Text = problemInfo[4], IsCorrect = problemInfo[4] == problemInfo[5] }
                        },
                        Hint = problemInfo.Count > 6 && !string.IsNullOrWhiteSpace(problemInfo[6]) ? problemInfo[6] : null,
                        PictureUrl = problemInfo.Count > 7 && !string.IsNullOrWhiteSpace(problemInfo[7]) ? problemInfo[7] : null,
                    };
                    problems.Add(problem);
                }
                var category = new Category
                {
                    CategoryName = int.Parse(kvp.Key),
                    Problems = problems
                };
                categories.Add(category);
            }
            return categories;
        }
    }

    public class Answer
    {
        public string Text { get; set; }
        
        public bool IsCorrect { get; set; }
    }

    public class Problem
    {
        public string QuestionText { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public string Hint { get; set; }
        public string PictureUrl { get; set; }
    }

    public class Category
    {
        public int CategoryName { get; set; }
        public ICollection<Problem> Problems { get; set; }
    }

    public class Subject
    {
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
    }

}
