using Microsoft.EntityFrameworkCore;
using ProductsApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductDbContext :DbContext
    {
        public ProductDbContext(DbContextOptions options ) : base(options) { 
       
        }
        public DbSet<Product> Products { get; set; }
    }
}
