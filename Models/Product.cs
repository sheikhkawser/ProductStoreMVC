using System;

namespace ProductStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
    }
}