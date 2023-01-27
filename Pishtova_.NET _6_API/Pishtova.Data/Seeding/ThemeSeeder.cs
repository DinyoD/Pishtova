namespace Pishtova.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.Extensions.DependencyInjection;

    using Pishtova.Common;
    using Pishtova.Services;
    using Pishtova.Data.Model;
    using Pishtova.Services.Models;

    public class ThemeSeeder : ISeeder
    {
        public async Task SeedAsync(PishtovaDbContext dbContext, IServiceProvider serviceProvider)
        {
            var helpers = serviceProvider.GetRequiredService<IHelpers>();
            var themeDTOs = helpers.CreateThemeDTOs(GlobalConstants.Bulgarian_12_Id);
            await this.SeedThemesAsync(dbContext, themeDTOs);
        }

        private async Task SeedThemesAsync(PishtovaDbContext dbContext, ICollection<ThemeDTO> themeDTOs)
        {
            foreach (var t in themeDTOs)
            {
                var sbj = dbContext.Subjects.FirstOrDefault(x => x.Id == t.SubjectId);
                var theme = new Theme { Name = t.Name, Subject = sbj};
                await dbContext.Themes.AddAsync(theme);
                foreach (var p in t.PoemDTOs)
                {
                    var author = dbContext.Authors.FirstOrDefault(x => x.Name == p.AuthorDTO.Name);
                    if (author == null)
                    {
                        author = new Author { Name = p.AuthorDTO.Name, PictureUrl = p.AuthorDTO.PictureUrl};
                        await dbContext.Authors.AddAsync(author);
                        await dbContext.SaveChangesAsync();
                    }
                    var poem = new Poem
                    {
                        Name = p.Name,
                        TextUrl= p.TextUrl,
                        AnalysisUrl= p.AnalysisUrl,
                        ExtrasUrl= p.ExtrasUrl,
                        Theme = theme,
                        Author= author,
                    };
                    await dbContext.Poems.AddAsync(poem);
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
