using System.Data.Entity;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.DAL
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {
            
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPriceChange> PriceChanges { get; set; }
    }
}
