using Infrastructure.Repository;
using ProductsApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services
{
    public class ProductService
    {
        private readonly ProductRepo productRepo;
        private readonly Queue<Product> active;
     
        
        
        
        
        public ProductService(ProductRepo productRepo, Queue<Product> active)
        {
            this.productRepo = productRepo;
            this.active = active;
        }
        public async Task<IEnumerable<Product>> SearchProduct(String name,DateTime t1,DateTime t2,Double Price1, Double Price2)
        {
            var res= await LatestProducts();
          return res.Where(x => x.Name == name && t1<=x.DatePosted && t2>=x.DatePosted && Price1<=x.Price && Price2>=x.Price).ToList();
      }
        public async Task<IEnumerable<Product>> LatestProducts()
        {
            var res = await productRepo.getProduct();
         return   res.OrderBy(x => x.DatePosted);
        }
        public void AddToQueue(Product potential)
        {
            
                active.Enqueue(potential);

        }
        public async Task<int> UpdateProduct(Product p)
        {
            Product o = await productRepo.getProductBasedId(p.Id);
            p.State = "Updated";
            if ( (p.Price-o.Price)/o.Price >0.5 || p.Price>5000)
            {
                AddToQueue(p);

            }
           

          

         
        

            return 1;
        }
        public async Task<int> CreateProduct(Product p)
        {
            if (p.Price > 10000)
                return 0;
            p.State = "Created";

            if (p.Price > 5000) {
                AddToQueue(p);
                return 1;
                }
            return 0;
           
               
        }
        public async Task<int> DeleteProduct(int id)
        {

            Product p =await productRepo.getProductBasedId(id);
            p.State = "Deleted";
            AddToQueue(p);
            return 1;
        }
    public Product getQueueFirst()
        {
            return active.Peek();
        }
    public async Task<int> Approve()
        {
            Product p=active.Dequeue();
            if (p.State.Equals("Deleted"))
                return await productRepo.DeleteAsync(p.Id);
            else if (p.State.Equals("Updated"))
                return await productRepo.UpdateAsync(p);

            return await productRepo.Insert(p);
        }
     public async Task<int> Reject()
        {
            Product p = active.Dequeue();

            return 1;
        }
     }
}
