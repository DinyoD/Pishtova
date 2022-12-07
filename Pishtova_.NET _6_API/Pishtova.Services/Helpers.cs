namespace Pishtova.Services
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using ProjectHelper;
    using Pishtova.Common;
    using Pishtova.Services.Models;

    using Sandbox;

    public class Helpers : IHelpers
    {
        public ICollection<SchoolDTO> ExtractAllSchoolsbyTownsAndMunicipality(string schoolInfoText)
        {
            var schoolsCollection = new List<SchoolDTO>();

            var collection = schoolInfoText.Split("},{");

            foreach (var item in collection)
            {
                if (!item.ToLower().Contains("детск") && !item.Contains("ДГ") && !item.ToLower().Contains("център") && !item.ToLower().Contains("градина") && !item.ToLower().Contains("общежитие"))
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
                        Name = FixSchoolName(schoolProps[5]),
                    };

                    schoolsCollection.Add(school);
                }
            }

            return schoolsCollection;
        }

        public SubjectDTO CreateSubjectDTO(string firebaseCollectionName, string subjectName, string subjectId)
        {
            var subjectInfo = ExtractSubjectProblems(firebaseCollectionName);

            ICollection<SubjectCategoryDTO> categories = new List<SubjectCategoryDTO>();
            for (int i = 0; i < subjectInfo.Count; i++)
            {
                var categoryInfo = subjectInfo[i];
                if (categoryInfo == null)
                {
                    continue;
                }
                ICollection<ProblemDTO> problems = new List<ProblemDTO>();
                for (int j = 0; j < categoryInfo.Count; j++)
                {
                    var problemInfo = categoryInfo[j];
                    if (problemInfo == null)
                    {
                        continue;
                    }
                    var problem = new ProblemDTO
                    {
                        QuestionText = problemInfo[0],
                        Answers = new List<AnswerDTO>{
                            new AnswerDTO { Text = problemInfo[1], IsCorrect = problemInfo[1] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[2], IsCorrect = problemInfo[2] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[3], IsCorrect = problemInfo[3] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[4], IsCorrect = problemInfo[4] == problemInfo[5] }
                        },
                        Hint = problemInfo.Count > 6 && !string.IsNullOrWhiteSpace(problemInfo[6]) ? problemInfo[6] : null,
                        PictureUrl = problemInfo.Count > 7 && !string.IsNullOrWhiteSpace(problemInfo[7]) ? problemInfo[7] : null,
                    };
                    problems.Add(problem);
                }
                //var categoryName = GlobalConstants.BiologyCategoriesName[i];

                var category = new SubjectCategoryDTO
                {
                    Name = "someCategory",
                    Problems = problems
                };
                categories.Add(category);
            }

            return new SubjectDTO
            {
                Id = subjectId,
                Name = subjectName,
                Categories = categories
            };
        }

        public SubjectDTO Create_Bio_SubjectDTO(string firebaseCollectionName, string subjectName, string subjectId)
        {
            var subjectInfo = ExtractSubjectProblems(firebaseCollectionName);
            //var bioErrors = subjectInfo.SelectMany( x => x).Where( x => x[1] != x[5] && x[2] != x[5] && x[3] != x[5] && x[4] != x[5]).ToList();

            ICollection<SubjectCategoryDTO> categories = new List<SubjectCategoryDTO>();
            for (int i = 0; i < subjectInfo.Count; i++)
            {
                var categoryInfo = subjectInfo[i];
                if (categoryInfo == null)
                {
                    continue;
                }
                ICollection<ProblemDTO> problems = new List<ProblemDTO>();
                for (int j = 0; j < categoryInfo.Count; j++)
                {
                    var problemInfo = categoryInfo[j];
                    if (problemInfo == null)
                    {
                        continue;
                    }
                    var problem = new ProblemDTO
                    {
                        QuestionText = problemInfo[0],
                        Answers = new List<AnswerDTO>{
                            new AnswerDTO { Text = problemInfo[1], IsCorrect = problemInfo[1] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[2], IsCorrect = problemInfo[2] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[3], IsCorrect = problemInfo[3] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[4], IsCorrect = problemInfo[4] == problemInfo[5] }
                        },
                        Hint = problemInfo.Count > 6 && !string.IsNullOrWhiteSpace(problemInfo[6]) ? problemInfo[6] : null,
                        PictureUrl = problemInfo.Count > 7 && !string.IsNullOrWhiteSpace(problemInfo[7]) ? problemInfo[7] : null,
                    };
                    problems.Add(problem);
                }
                var categoryName = GlobalConstants.BiologyCategoriesName[i];

                var category = new SubjectCategoryDTO
                {
                    Name = categoryName,
                    Problems = problems
                };
                categories.Add(category);
            }

            return new SubjectDTO
            {
                Id = subjectId,
                Name = subjectName,
                Categories = categories
            };
        }

        public SubjectDTO Create_Bg_SubjectDTO(string firebaseCollectionName, string subjectName, string subjectId)
        {
            var subjectInfo = Extract_Bg_SubjectProblems(firebaseCollectionName);

            ICollection<SubjectCategoryDTO> categories = new List<SubjectCategoryDTO>();
            foreach (var kvp in subjectInfo)
            {
                var categoryInfo = kvp.Value;
                if (categoryInfo == null)
                {
                    continue;
                }
                ICollection<ProblemDTO> problems = new List<ProblemDTO>();
                for (int j = 0; j < categoryInfo.Count; j++)
                {
                    var problemInfo = categoryInfo[j];
                    if (problemInfo == null)
                    {
                        continue;
                    }
                    var problem = new ProblemDTO
                    {
                        QuestionText = problemInfo[0],
                        Answers = new List<AnswerDTO>{
                            new AnswerDTO { Text = problemInfo[1], IsCorrect = problemInfo[1] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[2], IsCorrect = problemInfo[2] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[3], IsCorrect = problemInfo[3] == problemInfo[5] },
                            new AnswerDTO { Text = problemInfo[4], IsCorrect = problemInfo[4] == problemInfo[5] }
                        },
                        Hint = problemInfo.Count > 6 && !string.IsNullOrWhiteSpace(problemInfo[6]) ? problemInfo[6] : null,
                        PictureUrl = problemInfo.Count > 7 && !string.IsNullOrWhiteSpace(problemInfo[7]) ? problemInfo[7] : null,
                    };
                    problems.Add(problem);
                }

                var categoryName = GlobalConstants.BulgarianCategoriesName[int.Parse(kvp.Key)];
                var currentCategory = categories.FirstOrDefault(x => x.Name == categoryName);
                if (currentCategory != null)
                {
                    foreach (var problem in problems)
                    {
                        currentCategory.Problems.Add(problem);
                    }
                }
                else
                {
                    currentCategory = new SubjectCategoryDTO
                    {
                        Name = categoryName,
                        Problems = problems
                    };
                    categories.Add(currentCategory);

                }
            }

            return new SubjectDTO
            {
                Id = subjectId,
                Name = subjectName,
                Categories = categories
            };
        }

        private static List<List<List<string>>> ExtractSubjectProblems(string firebaseCollectionName)
        {
            var fbHelper = new FirebaseHelper(SandBoxConstants.AuthSecret, SandBoxConstants.BasePath);

            var subjectInfo = fbHelper.GetSubjectInfoFromFirebase(firebaseCollectionName);

            return subjectInfo;
        }

        private static Dictionary<string, List<List<string>>> Extract_Bg_SubjectProblems(string firebaseCollectionName)
        {
            var fbHelper = new FirebaseHelper(SandBoxConstants.AuthSecret, SandBoxConstants.BasePath);

            var subjectInfo = fbHelper.Get_BG_SubjectInfoFromFirebase(firebaseCollectionName);

            return subjectInfo;
        }

        private static string FixName(string name)
        {
            var result = name.Trim('"');
            return result;
        }

        private static string FixSchoolName(string name)
        {
            var result = name.Trim('"');
            if (!result.Contains(" \""))
            {
                result = result.Replace("\"", " \"");
            }
            if (result.Contains("\" "))
            {
                result = result.Replace("\" ", "\"");
            }
            if (result.Contains("св."))
            {
                result = result.Replace("св.", "Св.");
            }
            if (result.Contains("\""))
            {
                result += "\"";
            }
            if (result.Contains("ноучили"))
            {
                result = result.Replace("ноучили", "но учили");
            }
            if (result.Contains("назияпо"))
            {
                result = result.Replace("назияпо", "назия по");
            }
            if (!result.Contains(" по "))
            {
                result = result.Replace(" по", " по ");
            }
            result = AddSpacesToSentence(result);
            if (result.Contains("\" "))
            {
                result = result.Replace("\" ", "\"");
            }
            return result;
        }

        private static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ' && !char.IsUpper(text[i - 1]) )
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

    }
}
