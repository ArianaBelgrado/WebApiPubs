using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly pubsContext context;

        public PublisherController(pubsContext context)
        {
            this.context = context;
        }
        // Get
        [HttpGet]
        public ActionResult<IEnumerable<Publishers>> GetClinica()
        {
            return context.Publishers.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Publishers> GetbyId(string id)
        {
            Publishers publisher = (from p in context.Publishers
                                     where p.PubId == id
                                     select p).SingleOrDefault();

            return publisher;

        }

        //UPDATE
        //PUT api/publisher/{id}
        [HttpPut("{id}")]
        public ActionResult Put(string id, Publishers publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }

            context.Entry(publisher).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //INSERT
        [HttpPost]
        public ActionResult Post(Publishers publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Publishers.Add(publisher);
            context.SaveChanges();
            return Ok();
        }

        //DELETE
        //DELETE api/publisher/{id}
        [HttpDelete("{id}")]
        public ActionResult<Publishers> Delete(string id)
        {
            var publisher = (from p in context.Publishers
                             where p.PubId == id
                             select p).SingleOrDefault();

            if (publisher == null)
            {
                return NotFound();
            }

            context.Publishers.Remove(publisher);
            context.SaveChanges();

            return publisher;
        }
    }
}
