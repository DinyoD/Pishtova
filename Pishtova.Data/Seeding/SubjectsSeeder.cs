namespace Pishtova.Data.Seeding
{
    using Microsoft.Extensions.DependencyInjection;
    using Pishtova.Common;
    using Pishtova.Data.Model;
    using Pishtova.Services;
    using ProjectHelper;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubjectsSeeder : ISeeder
    {
        public async Task SeedAsync(PishtovaDbContext dbContext, IServiceProvider serviceProvider)
        {
            var helpers = serviceProvider.GetRequiredService<IHellpers>();
            if (dbContext.Subjects.Any(x => x.Name == GlobalConstants.BiologyBgName) == false)
            {
                var subjectDTO = helpers.Create_Bio_SubjectDTO(SandBoxConstants.Biology);
                await SeedSubjectProblemsAsync(dbContext, subjectDTO);
            }
            if (dbContext.Subjects.Any(x => x.Name == GlobalConstants.BulgarianBgName) == false)
            {
                var subjectDTO = helpers.Create_Bg_SubjectDTO(SandBoxConstants.Bulgarian);
                await SeedSubjectProblemsAsync(dbContext, subjectDTO);
            }
        }

        private async Task SeedSubjectProblemsAsync(PishtovaDbContext dbContext, Services.Models.SubjectDTO subjectDTO)
        {
            var subject = new Subject
            {
                Name = subjectDTO.Name,
                Categories = subjectDTO.Categories.Select(c => new SubjectCategory {
                    Name = c.Name,
                    Problems = c.Problems.Select(p => new Problem {
                        QuestionText = p.QuestionText,
                        PictureUrl = p.PictureUrl,
                        Hint = p.Hint,
                        Answers = p.Answers.Select(a => new Answer {
                            Text = a.Text,
                            IsCorrect = a.IsCorrect
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
            await dbContext.Subjects.AddAsync(subject);
            await dbContext.SaveChangesAsync();
        }
    }
}
