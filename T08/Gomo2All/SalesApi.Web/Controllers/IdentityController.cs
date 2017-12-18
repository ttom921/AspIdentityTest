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
        //[Authorize(Roles = "admin")]
        //[Authorize("admin")]

        //[Authorize(Policy = "dataEventRecordsAdmin")]
        //[Authorize(Policy = "admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
        //[Authorize(Roles = "user")]

        //[Authorize("admin")]

        //[Authorize(Policy = "dataEventRecordsUser")]
        
        [HttpGet]
        [Route("getfree")]
        [Authorize(Roles = "Admin,General")]
       // [Authorize(Policy = "admin")]
        //[Authorize(Policy = "user")]
        public IActionResult GetFreeUser()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}