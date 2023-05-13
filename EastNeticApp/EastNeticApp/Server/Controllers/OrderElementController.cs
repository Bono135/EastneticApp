using EastNeticApp.Server.Data;
using EastNeticApp.Shared.Model;
using EastNeticApp.Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EastNeticApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderElementController : ControllerBase
    {
        private readonly ApplicationDBContext _db;
        public OrderElementController(ApplicationDBContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ord = await _db.OrderElement.ToListAsync();
            return Ok(ord);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ord = await (from t1 in _db.OrderElement
                             join t2 in _db.Order on t1.OrderId equals t2.Id
                             where t2.Id == id
                             select new OrderElementsVM
                             {
                                 Id = t1.Id,
                                 ElementName = t1.ElementName,
                                 Quantity = t1.Quantity,
                                 TotalSubElement = t1.TotalSubElement,
                                 Name = t2.Name,
                                 OrderId = t2.Id
                             }).ToListAsync();
            return Ok(ord);
        }

        [HttpGet("{id1}/{id2}")]
        public async Task<IActionResult> Get(int id1, int id2)
        {
            var ord = await (from t1 in _db.OrderSubElement
                             join t2 in _db.OrderElement on t1.OrderElementId equals t2.Id
                             join t3 in _db.Order on t2.OrderId equals t3.Id
                             where t3.Id == id1 && t2.Id == id2
                             select new OrderSubElementsVM
                             {
                                 Id = t1.Id,
                                 Width = t1.Width,
                                 Height = t1.Height,
                                 ElementName = t2.ElementName,
                                 Quantity = t2.Quantity,
                                 TotalSubElement = t2.TotalSubElement,
                                 OrderElementId = t2.Id,
                                 orderType = (EnumOrderType) t1.OrderTypeId,
                                 Name = t3.Name,
                                 OrderId = t3.Id
                             }).ToListAsync();
            return Ok(ord);
        }
        [HttpGet("{id1}/{id2}/{id3}")]
        public async Task<IActionResult> Get(int id1, int id2, int id3)
        {
            var ord = await (from t1 in _db.OrderSubElement
                             join t2 in _db.OrderElement on t1.OrderElementId equals t2.Id
                             join t3 in _db.Order on t2.OrderId equals t3.Id
                             where t3.Id == id1 && t2.Id == id2 && t1.Id == id3
                             select new OrderSubElementsVM
                             {
                                 Id = t1.Id,
                                 Width = t1.Width,
                                 Height = t1.Height,
                                 ElementName = t2.ElementName,
                                 Quantity = t2.Quantity,
                                 TotalSubElement = t2.TotalSubElement,
                                 OrderElementId = t2.Id,
                                 Name = t3.Name,
                                 OrderId = t3.Id
                             }).ToListAsync();
            return Ok(ord);
        }
        [HttpPost]
        public async Task<IActionResult> Post(OrderElementsVM orders)
        {
            OrderElement ordd = new OrderElement
            {
                ElementName = orders.ElementName,
                Quantity = orders.Quantity,
                TotalSubElement = orders.TotalSubElement,
                OrderId = orders.OrderId,
            };
            _db.OrderElement.Add(ordd);
            await _db.SaveChangesAsync();
            return Ok(orders.OrderId);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostCreateSubElement(OrderSubElementsVM orders)
        {
            OrderSubElement ordd = new OrderSubElement
            {
                OrderElementId = orders.OrderElementId,
                OrderTypeId = (int)orders.orderType,
                Height = orders.Height,
                Width = orders.Width,
            };
            _db.OrderSubElement.Add(ordd);
            await _db.SaveChangesAsync();
            return Ok(orders.OrderElementId);
        }

        [HttpPut]
        public async Task<IActionResult> Put(OrderElement orders)
        {
            OrderElement ore = new OrderElement
            {
                ElementName = orders.ElementName,
                Quantity = orders.Quantity,
                TotalSubElement = orders.TotalSubElement,
                OrderId = orders.OrderId,
            };
            _db.OrderElement.Entry(ore).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> PutSubElement(OrderSubElementsVM orders)
        {
            OrderSubElement orse = new OrderSubElement
            {
                Id = orders.Id,
                OrderElementId = orders.OrderElementId,
                OrderTypeId = (int)orders.orderType,
                Height = orders.Height,
                Width = orders.Width,
            };
            _db.OrderSubElement.Entry(orse).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderElements = new OrderElement { Id = id };
            var orderSubElements = _db.OrderSubElement.Where(x => x.OrderElementId == id).ToList();
            _db.OrderElement.Remove(orderElements);
            _db.OrderSubElement.RemoveRange(orderSubElements);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteSubelement(int id)
        {
            var orderSubElement = new OrderSubElement { Id = id };
            _db.OrderSubElement.Remove(orderSubElement);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
