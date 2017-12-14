using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SalesApi.Web.Controllers
{
    //[Produces("application/json")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        [HttpGet]
        [Route("get")]
        //[Authorize(Roles = "PaidUser")]
        [Authorize(Policy = "dataEventRecordsAdmin")]
        //[Authorize("admin")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
        //[Authorize(Roles = "FreeUser")]
        //[Authorize(Policy = "dataEventRecordsUser")]
        //[Authorize("admin")]
        [HttpGet]
        [Route("getfree")]
        public IActionResult GetFreeUser()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}