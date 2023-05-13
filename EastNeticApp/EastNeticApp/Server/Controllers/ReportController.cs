using EastNeticApp.Server.Data;
using EastNeticApp.Shared.Model;
using EastNeticApp.Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EastNeticApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {

        private readonly ILogger<ReportController> _logger;

        private readonly ApplicationDBContext _db;
        public ReportController(ILogger<ReportController> logger, ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //List<OrderReportVM> model = new List<OrderReportVM>();
            var model = (from t1 in _db.Order
                     
                     select new OrderReportVM
                     {
                         Name = t1.Name,
                         State = t1.State,
                         ElementList = (from t2 in _db.OrderElement
                                        where t2.OrderId == t1.Id
                                        select new OrderElementsVM
                                        {
                                            ElementName = t2.ElementName,
                                            Quantity = t2.Quantity,
                                            TotalSubElement = t2.TotalSubElement,
                                            SubElementsList = (from t3 in _db.OrderSubElement
                                                               where t3.Active && t3.OrderElementId == t2.Id
                                                               select new OrderSubElementsVM
                                                               {
                                                                   orderType = (EnumOrderType)t3.OrderTypeId,
                                                                   Height = t3.Height,
                                                                   Width = t3.Width
                                                               }).ToList()
                                        }).ToList(),

                     }).ToList();

            return Ok(model);
        }
    }
}