using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsApplicationCore.Entities;

namespace ANSWER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;
        public ProductController(ProductService productService)
        {
           this.service = productService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Product model)
        {
            if (model != null)
            {
                return Ok(await service.CreateProduct(model));
            }
            return BadRequest();
        }
        [HttpGet("GetActiveProducts")]
        public async Task<IActionResult> getActiveProducts() {
            return Ok(await service.LatestProducts());
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await service.DeleteProduct(id));
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Product p){
            return Ok(await service.UpdateProduct(p));
        }
        [HttpGet("First")]
        public async Task<IActionResult> getFirstItemQueue()
        {
            return Ok(service.getQueueFirst());
        }
        [HttpGet("Accept")]
        public async Task<IActionResult> accept()
        {
            return Ok(await service.Approve());
        }
        [HttpGet("Reject")]
        public async Task<IActionResult> reject()
        {
            return Ok(await service.Reject());
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(String name,Double Price1,Double Price2, DateTime D1,DateTime D2)
        {
            return Ok(await service.SearchProduct(name, D1, D2, Price1, Price2));
        }
    }
}
