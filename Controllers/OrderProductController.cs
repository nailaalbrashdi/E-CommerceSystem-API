using E_CommerceSystem_API.DTOs;
using E_CommerceSystem_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem_API.Controllers
{




    [ApiController]
    [Route("api/OrderProduct")]
    public class OrderProductController: ControllerBase
    {

        ApplicationDbContext context = new ApplicationDbContext();

        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder(PlaceOrderDTO dto)
        {
            var user = context.Users.FirstOrDefault(u => u.UserId == dto.UserId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            Order order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = 0
            };

            context.Orders.Add(order);
            context.SaveChanges();

            List<OrderProducts> orderProducts = new List<OrderProducts>();

            foreach (var item in dto.Items)
            {
                orderProducts.Add(new OrderProducts
                {
                    OrderId = order.OrderId,
                    ProductId = item.Pid,
                    Quantity = item.qnt
                });
            }

            context.OrderProducts.AddRange(orderProducts);
            context.SaveChanges();

            return Ok("Order placed successfully");
        }


    }
}
