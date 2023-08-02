using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ProductsApplicationCore.Entities;

namespace Infrastructure.Repository
{
    public class ProductRepo
    {
        private ProductDbContext productDbContext;
        public ProductRepo(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;
        }
       public async Task<IEnumerable<Product>> getProduct()
        {
           return await productDbContext.Set<Product>().ToListAsync();
        }
        public async Task<int> Insert(Product entity)
        {
            productDbContext.Set<Product>().Add(entity);
            return await productDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            productDbContext.Set<Product>().Update(entity);
            return await productDbContext.SaveChangesAsync();
        }
        public async Task<Product> getProductBasedId(int id)
        {
         return await productDbContext.Set<Product>().FirstAsync(x => x.Id == id);
        }
        public async Task<int> DeleteAsync(int id){
            Product t = await getProductBasedId(id);
            if (t != null)
            {
                productDbContext.Set<Product>().Remove(t);
                return await productDbContext.SaveChangesAsync();
            }
            return 0;
        }


    }
}
