using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using theatreers.shared.Models;
using theatreers.shared.Interfaces;

namespace theatreers.venue.Controllers
{
    //[Authorize]
    [Route("venues")]
    [ApiController]
    public class VenuesController : ControllerBase
    {

        private readonly IVenueService _service;
    
        public VenuesController(IVenueService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Venue>> Get()
        {               
            var items = _service.GetAll();
            return Ok(items);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Venue> Get(int id)
        {   
            var item = _service.GetById(id);
    
            if (item == null)
            {
                return NotFound();
            }
    
            return Ok(item);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Venue body)
        {      
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
            Venue item = _service.Create(body);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Venue> Put([FromBody] Venue body)
        {                
            var existingItem = _service.GetById(body.Id);
 
            if (existingItem == null)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
            _service.Update(body);
            return Ok(body);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingItem = _service.GetById(id);
 
            if (existingItem == null)
            {
                return NotFound();
            }
    
            _service.Delete(id);
            return Ok();
        }
    }
}
