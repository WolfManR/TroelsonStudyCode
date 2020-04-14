using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoLotDAL_Core2.EF;
using AutoLotDAL_Core2.Models;
using AutoLotDAL_Core2.Repos;
using AutoMapper;
using Newtonsoft.Json;

namespace AutoLotAPI_Core2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepo _repo;
        private IMapper mapper;

        public InventoryController(IInventoryRepo repo)
        {
            _repo = repo;
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Inventory, Inventory>().ForMember(x => x.Orders, opt => opt.Ignore())).CreateMapper();
        }

        // GET: api/Inventory
        [HttpGet]
        public IEnumerable<Inventory> GetCars()
        {
            var inventories = _repo.GetAll();
            return mapper.Map<List<Inventory>, List<Inventory>>(inventories);
        }

        // GET: api/Inventory/5
        [HttpGet("{id}", Name ="DisplayRoute")]
        public async Task<IActionResult> GetInventory([FromRoute]int id)
        {
            Inventory inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Inventory,Inventory>(inventory));
        }

        // PUT: api/Inventory/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory([FromRoute]int id, [FromBody]Inventory inventory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != inventory.Id) return BadRequest();
            _repo.Update(inventory);
            return NoContent();
        }

        // POST: api/Inventory
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<IActionResult> PostInventory([FromBody]Inventory inventory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _repo.Add(inventory);
            return CreatedAtAction("DisplayRoute", new { id = inventory.Id }, inventory);
        }

        // DELETE: api/Inventory/5
        [HttpDelete("{id}/{timestamp}")]
        public async Task<IActionResult> DeleteInventory([FromRoute]int id,[FromRoute] string timestamp)
        {
            if (!timestamp.StartsWith("\"")) timestamp = $"\"{timestamp}\"";
            var ts = JsonConvert.DeserializeObject<byte[]>(timestamp);
            _repo.Delete(id, ts);
            return Ok();
        }
    }
}
