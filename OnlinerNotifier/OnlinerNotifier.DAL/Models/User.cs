using System.Collections.Generic;

namespace OnlinerNotifier.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public User()
        {
            Products = new List<Product>();
        }
    }
}
