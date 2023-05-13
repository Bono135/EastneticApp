using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastNeticApp.Shared.Model
{
    public class Order : Base
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }

    public class OrderElement : Base
    {
        public string ElementName { get; set; } = "";
        public int Quantity { get; set; }
        public int TotalSubElement { get; set; }
        public int  OrderId { get; set; }
    }

    public class OrderSubElement : Base
    {
        public int OrderTypeId { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int OrderElementId { get; set; }
    }

    public abstract class Base
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
    }

    public enum EnumOrderType
    {
        Doors = 1,
        Windows
    }
}
