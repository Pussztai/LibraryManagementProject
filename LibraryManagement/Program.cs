
using LibraryManagement.Constants;
using LibraryManagement.Data;
using LibraryManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionstring = builder.Configuration.GetConnectionString("LibraryManagementDbConnectionString");
            builder.Services.AddDbContext<LibraryManagementDbContext>(options => options.UseSqlServer(connectionstring));

            // Add services to the container.
            builder.Services.AddScoped<IBookService, BookService>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
