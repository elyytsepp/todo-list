using Microsoft.EntityFrameworkCore;
using TodoAPI.Models.Data;
using TodoAPI.Models.Repositories;


namespace TodoAPI
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

            // Adds CORS to the API to allow calls from a frontend.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // To make sure the backend is not confined to localhost.
            builder.WebHost.UseUrls("http://0.0.0.0:8080");


            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<TodoAPIDBContext>(options =>
            {
                options.UseSqlite("Data Source=todo.db");
            });

            builder.Services.AddScoped<ITodoRepository, TodoRepository>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TodoAPIDBContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
