using E_CommerceSystem_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using E_CommerceSystem_API.DTOs;


namespace E_CommerceSystem_API.Controllers
{


    [ApiController]
    [Route("api/Review")]
    public class ReviewController : ControllerBase
    {
        ApplicationDbContext context = new ApplicationDbContext();



        [HttpPost("AddReview")]
        public IActionResult AddReview(AddReviewDTO reviewdto)
        {
            var product = context.Products
                .FirstOrDefault(p => p.ProductId == reviewdto.ProductId);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            var user = context.Users
                .FirstOrDefault(u => u.UserId == reviewdto.UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            Review r = new Review()
            {
                ProductId = reviewdto.ProductId,
                UserId = reviewdto.UserId,
                Rating = reviewdto.Rating,
                Comment = reviewdto.Comment,
                ReviewDate = DateTime.Now
            };

            context.Reviews.Add(r);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }

            return Ok("Review added successfully with ID : " + r.ReviewId);
        }



        [HttpGet("GetAllReview")]
        public IActionResult ListReview()
        {
            var reviews = context.Reviews
                .Select(r => new
                {
                    ReviewId = r.ReviewId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate,
                    UserId = r.UserId,
                    UserName = r.User.Name,      
                    ProductId = r.ProductId,
                    ProductName = r.product.Name
                })
                .ToList();

            return Ok(reviews);
        }




        [HttpPut("UpdateReview")]
        public IActionResult UpdateReview(UpdateReviewDTO reviewDTO)
        {
            var review = context.Reviews
                .FirstOrDefault(r => r.ReviewId == reviewDTO.ReviewId);

            if (review == null)
            {
                return NotFound("Review not found");
            }

            review.Rating = reviewDTO.Rating;
            review.Comment = reviewDTO.Comment;
            review.ReviewDate = reviewDTO.ReviewDate;

            context.SaveChanges();

            return Ok("Review updated successfully with ID: " + review.ReviewId);
        }

        [HttpDelete("RemoveReview")]
        public IActionResult RemoveReview(int id)
        {
            var review = context.Reviews.Find(id);
            if (review != null)
            {
                context.Reviews.Remove(review);
                context.SaveChanges();
                return Ok("Review removed successfully");
            }

            return NotFound("Review not found");

        }



    }
}
