using EastNeticApp.Shared.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EastNeticApp.Shared.ViewModel
{
    public class OrderVM : BaseVM
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }

    public class OrderElementsVM : OrderVM
    {
        public string ElementName { get; set; } = "";
        public int Quantity { get; set; }
        public int TotalSubElement { get; set; }
        public int OrderId { get; set; }
        public List<OrderSubElementsVM> SubElementsList { get; set; } = new List<OrderSubElementsVM>();

    }
    public class OrderSubElementsVM : OrderElementsVM
    {
        public int OrderyTypeId { get; set; }
        public EnumOrderType orderType { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int OrderElementId { get; set; }
    }
    public abstract class BaseVM
    {
        public int Id { get; set; }
        public bool Active { get; set; }
    }

}
