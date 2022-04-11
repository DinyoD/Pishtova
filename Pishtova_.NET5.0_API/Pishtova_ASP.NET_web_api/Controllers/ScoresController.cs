namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.Score;
    using Pishtova_ASP.NET_web_api.Model.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ScoresController: ApiController
    {
        private readonly IScoreService scoreService;
        private readonly IUserService userService;

        public ScoresController(
            IScoreService scoreService,
            IUserService userService)
        {
            this.scoreService = scoreService;
            this.userService = userService;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Save([FromBody] ScoreDTO data)
        {
            if (data == null)
            {
                return StatusCode(400, new ErrorResult { Message = "The request body is empty" });
            }
            var userId = await this.userService.GetUserIdAsync(User);
            if (userId == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Unauthorized request" });
            }
            var score = new ScoreModel
            {
                UserId = userId,
                SubjectCategoryId = data.SubjectCategoryId,
                Points = data.Points
            };
            await this.scoreService.SaveScoreInDbAsync(score);
            return StatusCode(201);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ICollection<SubjectPointsModel>> UserPointsBySubject()
        {
            var userId = await this.userService.GetUserIdAsync(User);
            return await this.scoreService.GetUserSubjectScoresAsync(userId);
        }

        [HttpGet]
        [Route("[action]/{sbjId}")]
        public async Task<ICollection<CategoryWithPointsModel>> SubjectPointsByCategories(int sbjId)
        {
            var userId = await this.userService.GetUserIdAsync(User);
            var result =  await this.scoreService.GetSubjectCategoriesScoresAsync(userId, sbjId);
            return result;
        }
    }
}
