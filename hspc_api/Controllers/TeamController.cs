using System;
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
               
                var dbTeam = _dbContext.Teams;
                return Ok(dbTeam);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [HttpGet]
        [Route("/teams/{id}")]
        public async Task<object> GetTeam([FromRoute] int id)
        {
            try
            {

                var dbTeam = await _dbContext.Teams.FindAsync(id);
                return Ok(dbTeam);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
