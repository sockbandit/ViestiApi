
using Microsoft.EntityFrameworkCore;
using ViestiApi.Models;
using ViestiApi.Repositories;
using ViestiApi.Services;

namespace ViestiApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            
            builder.Services.AddScoped<IMessageRepository, MessageRepository>(); // Lis‰‰ viestivaraston palveluna
            builder.Services.AddScoped<IMessageService, MessageService>(); // Lis‰‰ viestipalvelun palveluna
            builder.Services.AddScoped<IUserRepository, UserRepository>(); // Lis‰‰ k‰ytt‰j‰varaston palveluna
            builder.Services.AddScoped<IUserService, UserService>(); // Lis‰‰ k‰ytt‰j‰palvelun palveluna

            builder.Services.AddDbContext<MessageServiceContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ViestiDB"))); // Lis‰‰ tietokantayhteyden palveluna, k‰ytt‰en SQL Serveri‰ ja ViestiDB-yhteysmerkkijonoa


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
