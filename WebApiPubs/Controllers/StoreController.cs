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
        public ActionResult<IEnumerable<Sales>> GetClinica()
        {
            return context.Sales.ToList();
        }
        //UPDATE
        //PUT api/store/{id}
        [HttpPut("{id}")]
        public ActionResult Put(string id, Sales Sales)
        {
            if (id != Sales.StorId)
            {
                return BadRequest();
            }

            context.Entry(Sales).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //INSERT
        [HttpPost]
        public ActionResult Post(Sales Sales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Sales.Add(Sales);
            context.SaveChanges();
            return Ok();
        }

        //DELETE
        //DELETE api/store/{id}
        [HttpDelete("{id}")]
        public ActionResult<Sales> Delete(string id)
        {
            var Sales = (from c in context.Sales
                             where c.StorId == id
                             select c).SingleOrDefault();

            if (Sales == null)
            {
                return NotFound();
            }

            context.Sales.Remove(Sales);
            context.SaveChanges();

            return Sales;
        }
    }
}
