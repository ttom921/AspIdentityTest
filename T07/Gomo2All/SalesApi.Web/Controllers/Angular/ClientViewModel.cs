using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApi.Web.Controllers.Angular
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
