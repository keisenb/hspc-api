using System;
using System.Linq;
using System.Threading.Tasks;
using hspc_api.Data;
using hspc_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hspc_api.Controllers
{
    [Authorize(Roles = Roles.ROLE_JUDGE + "," + Roles.ROLE_ADMINISTRATOR )]
    public class TeamController : Controller
    {

        private readonly UserDbContext _dbContext;

        public TeamController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("/teams")]
        public object GetTeams() {
            try
            {
               
                var dbTeam = _dbContext.Teams.ToList();
                return Ok(dbTeam);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [HttpGet]
        [Route("/teams/{problemId}/beginner")]
        public object GetBeginnerTeams([FromRoute] int problemId)
        {
            try
            {

                var dbTeam = _dbContext.Teams.Where(x => x.Beginner == true).ToList(); //todo join TeamProblems and add problem id to query

                return Ok(dbTeam);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("/teams/advanced")]
        public object GetAdvancedTeams()
        {
            try
            {

                var dbTeam = _dbContext.Teams.Where(x => x.Advanced == true).ToList(); //todo same as beginner
                return Ok(dbTeam);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("/teams/{id}")]
        public object GetTeam([FromRoute] int id)
        {
            try
            {

                var dbTeam = _dbContext.Teams.Where(x=> x.Id == id).FirstOrDefault();
                return Ok(dbTeam);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
