﻿
namespace OnlinerNotifier.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int OnlinerId { get; set; }

        public int Name { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal MinPrice { get; set; }
    }
}
