using System;
using System.Threading.Tasks;
using hspc_api.Data;
using hspc_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hspc_api.Controllers
{
    [Authorize(Roles = Roles.ROLE_JUDGE + "," + Roles.ROLE_ADMINISTRATOR)]
    public class ProblemController : Controller
    {

        private readonly UserDbContext _dbContext;

        public ProblemController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("/problems")]
        public object GetProblems()
        {
            try
            {
                var result = _dbContext.Problems;
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("/problems/{id}")]
        public async Task<object> GetProblem([FromRoute] int id)
        {
            try
            {

                var result = await _dbContext.Problems.FindAsync(id);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
