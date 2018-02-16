using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hspc_api.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hspc_api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IArticleService articleService;
        private readonly UserManager<IdentityUser> _userManager;


        public ValuesController(IArticleService articleService, UserManager<IdentityUser> userManager) {
            this.articleService = articleService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<object> Protected()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return new JsonResult(user);
        }
    }
}
