using EastNeticApp.Server.Data;
using EastNeticApp.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EastNeticApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDBContext _db;
        public OrderController(ApplicationDBContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ord = await _db.Order.ToListAsync();
            return Ok(ord);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ord = await _db.Order.Where(x=>x.Id == id).ToListAsync();
            return Ok(ord);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Order orders)
        {
            _db.Order.Add(orders);
            await _db.SaveChangesAsync();
            return Ok(orders.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Order orders)
        {
            _db.Order.Entry(orders).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orders = new Order { Id = id };
            var orderElements = _db.OrderElement.Where(x => x.OrderId == id).ToList();
            foreach (var orderElement in orderElements)
            {
                var ordersubelements = _db.OrderSubElement.Where(x => x.OrderElementId == orderElement.Id).ToList();
                _db.OrderSubElement.RemoveRange(ordersubelements);
            }
            _db.Order.Remove(orders);
            _db.OrderElement.RemoveRange(orderElements);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
