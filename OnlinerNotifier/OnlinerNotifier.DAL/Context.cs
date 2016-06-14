using System.Data.Entity;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.DAL
{
    class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Product> Products { get; set; }
    }
}
