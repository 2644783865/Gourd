using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gourd.IdentityApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
      

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetUser()
        {
            var mm = User.Identity.Name;
            var userinfo = from c in User.Claims select new { c.Type, c.Value };
            return new JsonResult(userinfo);
        }
    }
}
