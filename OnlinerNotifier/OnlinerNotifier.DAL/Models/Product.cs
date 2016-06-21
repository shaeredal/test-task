
using System.Collections.Generic;

namespace OnlinerNotifier.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int OnlinerId { get; set; }

        public string Name { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal MinPrice { get; set; }

        public ICollection<User> Users { get; set; }

        public Product()
        {
            Users = new List<User>();
        }
    }
}
