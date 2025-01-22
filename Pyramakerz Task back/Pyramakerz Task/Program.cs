
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pyramakerz_Task.BL.UOW;
using Pyramakerz_Task.Models;

namespace Pyramakerz_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PyramakerzTaskContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("PT")));
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
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

            app.UseCors(txt);
            app.MapControllers();

            app.Run();
        }
    }
}
