using EastNeticApp.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace EastNeticApp.Server.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderElement> OrderElement { get; set; }
        public DbSet<OrderSubElement> OrderSubElement { get; set; }
    }
}
