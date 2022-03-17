namespace Pishtova_ASP.NET_web_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pishtova.Data.Model;
    using Pishtova.Services.Data;
    using Pishtova_ASP.NET_web_api.Model.Results;
    using Pishtova_ASP.NET_web_api.Model.Score;
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
        [Route(nameof(Save))]
        public async Task<IActionResult> Save([FromBody] ScoreDTO data)
        {
            if (data == null)
            {
                return StatusCode(400, new ErrorResult { Message = "The request body is empty" });
            }
            var userId = this.userService.GetUserId(User);
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
    }
}
