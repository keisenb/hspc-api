using System;
using System.Linq;
using System.Threading.Tasks;
using hspc_api.Data;
using hspc_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hspc_api.Controllers
{
    [Authorize(Roles = Roles.ROLE_JUDGE + "," + Roles.ROLE_ADMINISTRATOR)]
    public class JudgeController : Controller
    {
        private readonly UserDbContext _dbContext;

        public JudgeController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        [Route("/judge/mark")]
        public async Task<object> MarkTeamForJudgingAsync([FromBody] MarkTeamForJudgeDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if(_dbContext.TeamProblems.Where(x => x.Problem.Id == model.ProblemId && x.Team.Id == model.TeamId).FirstOrDefault() != null) 
                {
                    return BadRequest(new { message = "Team already marked" });
                }

                var team = _dbContext.Teams.Where(x => x.Id == model.TeamId).FirstOrDefault();
                if (team == null)
                {
                    return BadRequest(new { message = "Invalid team id" });
                }
                var problem = _dbContext.Problems.Where(x => x.Id == model.ProblemId).FirstOrDefault();
                if (problem == null)
                {
                    return BadRequest(new { message = "Invalid problem id" });
                }
                var teamProblem = new TeamProblems
                {
                    Team = team,
                    Problem = problem,
                    MarkedForJudging = true

                };
                var entity = await _dbContext.TeamProblems.AddAsync(teamProblem);
                var result = await _dbContext.SaveChangesAsync();
                if(result == 1) {
                    return Ok(entity.Entity);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
