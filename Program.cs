
namespace E_CommerceSystem_API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //building phase

            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.

            builder.Services.AddControllers();


            // configure Swagger/OpenAPI for API documentation and testing
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // end of building phase



            //running phase

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            //end of running phase
        }
    }
}
