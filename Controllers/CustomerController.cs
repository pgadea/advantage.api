using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route ("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApiContext _ctx;

        public CustomerController (ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get ()
        {
            var data = _ctx.Customers.OrderBy (c => c.Id);

            return Ok (data);
        }

        [HttpGet ("{id}", Name = "GetCustomer")]
        public IActionResult Get (int id)
        {
            var customer = _ctx.Customers.Find (id);
            return Ok (customer);
        }

        [HttpPost]
        public IActionResult Post ([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest ();
            }

            _ctx.Customers.Add (customer);
            _ctx.SaveChanges ();

            return CreatedAtRoute ("GetCustomer", new { id = customer.Id }, customer);
        }
    }
}