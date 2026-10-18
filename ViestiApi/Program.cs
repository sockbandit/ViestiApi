
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
            
            builder.Services.AddSingleton<IMessageRepository, MessageRepository>(); // Lis‰‰ viestivaraston palveluna
            builder.Services.AddScoped<IMessageService, MessageService>(); // Lis‰‰ viestipalvelun palveluna
            builder.Services.AddSingleton<IUserRepository, UserRepository>(); // Lis‰‰ k‰ytt‰j‰varaston palveluna
            builder.Services.AddScoped<IUserService, UserService>(); // Lis‰‰ k‰ytt‰j‰palvelun palveluna

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
