using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;
        }
        // Get
        [HttpGet]
        public ActionResult<IEnumerable<Stores>> GetAll()
        {
            return context.Stores.ToList();
        }


        //get por id 
        [HttpGet("{id}")]
        public ActionResult<Stores> GetbyId(string id)
        {
            Stores Stores = (from p in context.Stores
                             where p.StorId == id
                              select p).SingleOrDefault();

            return Stores;

        }
        //UPDATE
        //PUT api/store/{id}
        [HttpPut("{id}")]
        public ActionResult Put(string id, Stores Stores)
        {
            if (id != Stores.StorId)
            {
                return BadRequest();
            }

            context.Entry(Stores).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //INSERT
        [HttpPost]
        public ActionResult Post(Stores stores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Stores.Add(stores);
            context.SaveChanges();
            return Ok();
        }

        //DELETE
        //DELETE api/store/{id}
        [HttpDelete("{id}")]
        public ActionResult<Stores> Delete(string id)
        {
            var Stores = (from c in context.Stores
                          where c.StorId == id
                             select c).SingleOrDefault();

            if (Stores == null)
            {
                return NotFound();
            }

            context.Stores.Remove(Stores);
            context.SaveChanges();

            return Stores;
        }
    }
}
