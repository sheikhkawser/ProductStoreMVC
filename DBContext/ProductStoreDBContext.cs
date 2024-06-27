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
    }
}
