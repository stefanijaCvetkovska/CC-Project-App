using Microsoft.Extensions.DependencyInjection;
using web_api.Data;
using web_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//My Services
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddSingleton<ResultService>();
builder.Services.AddCors();
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//My Services
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
//

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
var urls = new[] { "http://0.0.0.0:7168", "http://0.0.0.0:5137" };
app.Run(urls.FirstOrDefault());

