using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductStore.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductStore.DBContext
{
    public class ProductStoreDBContext : DbContext
    {
        public ProductStoreDBContext(DbContextOptions<ProductStoreDBContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //SeedData(modelBuilder);
        //}

        //private void SeedData(ModelBuilder modelBuilder)
        //{
        //    var productData = File.ReadAllText("Data/products.json");
        //    var products = JsonConvert.DeserializeObject<List<Product>>(productData);

        //    var productEntities = products.Select((p, index) => new Product
        //    {
        //        Id = index+1,
        //        Name = p.Name,
        //        Category = p.Category,
        //        Description = p.Description,
        //        FileName = p.FileName,
        //        Height = p.Height,
        //        Width = p.Width,
        //        Price = p.Price,
        //        Rating = p.Rating
        //    }).ToList();

        //    modelBuilder.Entity<Product>().HasData(productEntities);
        //}
    }
}
