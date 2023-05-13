using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastNeticApp.Shared.ViewModel
{
    public class OrderReportVM
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        public List<OrderElementsVM> ElementList { get; set; } = new List<OrderElementsVM>();

        //public IEnumerable<OrderSubElementsVM> SubElementsList { get; set; } =  Enumerable.Empty<OrderSubElementsVM>();

    }
}
