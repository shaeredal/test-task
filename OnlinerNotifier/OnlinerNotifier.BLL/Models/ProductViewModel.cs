using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerNotifier.BLL.Models
{
    public class ProductViewModel
    {
        public int OnlinerId { get; set; }

        public int Name { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal MinPrice { get; set; }
    }
}
