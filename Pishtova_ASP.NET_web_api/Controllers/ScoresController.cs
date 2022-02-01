using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pishtova.Data.Model;
using Pishtova.Services.Data;
using Pishtova_ASP.NET_web_api.Model.Results;
using Pishtova_ASP.NET_web_api.Model.Score;
using System.Linq;
using System.Threading.Tasks;

namespace Pishtova_ASP.NET_web_api.Controllers
{
    public class ScoresController: ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IScoreService scoreService;
        
        public ScoresController(UserManager<User> userManager, IScoreService scoreService)
        {
            this.userManager = userManager;
            this.scoreService = scoreService;
        }


        [HttpPost]
        [Route(nameof(AddScore))]
        public async Task<IActionResult> AddScore([FromBody] AddScoreModel  scoreModel)
        {
            if (scoreModel == null)
            {
                return StatusCode(400, new ErrorResult { Message = "The request body is empty" });
            }
            var userId = User.Claims.FirstOrDefault(i => i.Type == "userId").Value;
            if (userId == null)
            {
                return StatusCode(400, new ErrorResult { Message = "Unauthorized request" });
            }
            var score = new Score
            {
                UserId = userId,
                SubjectCategoryId = scoreModel.SubjectId,
                Points = scoreModel.Points
            };
            await this.scoreService.AddScoreInDbAsync(score);
            return StatusCode(201);
        }
    }
}
