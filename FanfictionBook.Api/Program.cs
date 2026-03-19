
using FanfictionBook.Application.Interfaces.Repositories;
using FanfictionBook.Application.Interfaces.Services;
using FanfictionBook.Application.Services;
using FanfictionBook.Infrastructure.Data;
using FanfictionBook.Infrastructure.Repositories;
using FanfictionBook.Application.Interfaces.Helpers;
using Microsoft.EntityFrameworkCore;
using FanfictionBook.Infrastructure.Helpers;

namespace FanfictionBook.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //=====================================autoMapper=====================================
            builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            //==============================Repositories=====================================
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            //==============================Services=========================================
            builder.Services.AddScoped<IUserService, UserService>();

            //==============================Helpers==========================================
            builder.Services.AddScoped<IHashHelper, HashHelper>();



            builder.Services.AddDbContext<FanfictionBookContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
