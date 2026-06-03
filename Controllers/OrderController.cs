using E_CommerceSystem_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace E_CommerceSystem_API.Controllers
{


    [ApiController]
    [Route("api/Order")]
    public class OrderController : ControllerBase
    {
        ApplicationDbContext context = new ApplicationDbContext();



        [HttpGet("GetAllOrdersForUser")]
        public IActionResult ListOrderForUser(int userId)
        {
            var orders = context.Orders
                .Where(o => o.UserId == userId)
                .ToList();

            var result = new List<object>();

            foreach (var order in orders)
            {
                var products = context.OrderProducts
                    .Where(op => op.OrderId == order.OrderId)
                    .ToList();

                foreach (var product in products)
                {
                    var productInfo = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

                    var userInfo = context.Users.FirstOrDefault(u => u.UserId == order.UserId);

                    result.Add(new
                    {
                        order.UserId,
                        UserName = userInfo?.Name,
                        order.OrderId,
                        product.ProductId,
                        ProductName = productInfo?.Name,
                        product.Quantity
                    });
                }
            }

            if (!result.Any())
            {
                return NotFound("No orders found for this user.");
            }

            return Ok(result);
        }




        [HttpGet("GetOrderDetailsById")]
        public IActionResult GetOrderById(int id)
        {
            var order = context.Orders
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var user = context.Users
                .FirstOrDefault(u => u.UserId == order.UserId);

            var result = context.OrderProducts
                .Where(op => op.OrderId == id)
                .Select(op => new
                {
                    order.OrderId,
                    order.OrderDate,
                    order.UserId,
                    UserName = user.Name,

                    op.ProductId,
                    ProductName = op.Product.Name,
                    ProductPrice = op.Product.Price,
                    op.Quantity
                })
                .ToList();

            return Ok(result);
        }




    }

    }
