using Microsoft.EntityFrameworkCore;
using WebAPI_CRUDOperations.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BrandContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BrandCS"))
);

var app = builder.Build();
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
