using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API
{
    [Route ("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly ApiContext _ctx;

        public ServerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _ctx.Servers.OrderBy (s => s.Id).ToList ();
            return Ok (response);
        }

        [HttpGet ("{id}", Name = "GetServer")]
        public IActionResult Get(int id)
        {
            var response = _ctx.Servers.Find (id);
            return Ok ();
        }

        [HttpPut ("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = _ctx.Servers.Find(id);

            if (server == null)
            {
                return NotFound();
            }

            // Refactor: move into a service
            if (msg.Payload == "activate")
            {
                server.IsOnline = true;
            }

            if (msg.Payload == "deactivate")
            {
                server.IsOnline = false;
            }

            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}