using System.Collections.Generic;
using IdentityServer.Api1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "ReadProduct")]
        public IActionResult GetProducts()
        {
            var productList = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Macbook Pro",
                    Price = 4000,
                    Stock = 5
                }
            };
            return Ok(productList);
        }

        [Authorize(Policy = "UpdateOrCreate")]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"Id' si {id} olan ürün güncellenmiştir.");
        }
        
        [Authorize(Policy = "UpdateOrCreate")]
        public IActionResult CreateProduct(Product product)
        {
            return Ok(product);
        }
    }
}