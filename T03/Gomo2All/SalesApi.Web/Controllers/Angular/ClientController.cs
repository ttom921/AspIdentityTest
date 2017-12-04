using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SalesApi.Web.Controllers.Angular
{
    [Produces("application/json")]
    [Route("api/Client")]
    public class ClientController : Controller
    {
        List<ClientViewModel> _AllClient = new List<ClientViewModel>();
        public ClientController()
        {
            Random rNumber = new Random();
            for (int i = 0; i < 3; i++)
            {
                ClientViewModel item = new ClientViewModel();
                item.Id = i;
                item.FirstName = "First_" + i;
                item.LastName = "LastName_" + i;
                item.Email = i + "@gomo2o.com";
                item.Balance = rNumber.Next(50000);
                item.Phone = "phone_" + i;
                _AllClient.Add(item);
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var results = (from a in _AllClient
                           select new
                           {
                               id = a.Id,
                               firstName = a.FirstName,
                               lastName = a.LastName,
                               email = a.Email,
                               phone = a.Phone,
                               balance = a.Balance
                           });

            // var results = Json(results);
            return Ok(results);
            // return Ok(Json(results));
        }
    }
}