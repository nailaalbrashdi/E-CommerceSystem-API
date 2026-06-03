using E_CommerceSystem_API.DTOs;
using E_CommerceSystem_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace E_CommerceSystem_API.Controllers
{



    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        ApplicationDbContext context = new ApplicationDbContext();


        [HttpPost("AddProduct")]
        public IActionResult AddProduct(AddProductDTO productdto)
        {
            Product p=new Product();
            p.Name = productdto.Name;
            p.Description = productdto.Description;
            p.Price = productdto.Price;
            p.Stock = productdto.Stock;


            context.Products.Add(p);
            context.SaveChanges();
            
            return Ok(" Product added successfully with ID : " + p.ProductId);
        }


        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(UpdateProductDTO updatedto)
        {
            var p = context.Products.FirstOrDefault(x => x.ProductId == updatedto.ProductId);

            if (p == null)
            {
                return NotFound("Product not found");
            }

            p.Description = updatedto.Description;
            p.Price = updatedto.Price;
            p.Stock = updatedto.Stock;

            context.SaveChanges();

            return Ok("Product updated successfully with ID: " + p.ProductId);
        }

        [HttpGet("GetListOfProducts")]
        public IActionResult ListProducts()
        {
            var Product= context.Products.ToList();
            return Ok(Product);
        }



        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            var Product = context.Products.Find(id);
            if (Product== null)
            {
                return NotFound("Product not found");
            }
            return Ok( Product);
        }




    }
}
