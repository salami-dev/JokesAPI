using JokesAPI.Data;
using Microsoft.EntityFrameworkCore;

using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Filters;
using Microsoft.OpenApi.Models;
using JokesAPI.Exceptions;
using JokesAPI.Interface;
using JokesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultDBConnection");

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
builder.Services.AddDbContext<JokeContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(

       options =>
       {
           options.SwaggerDoc("v1", new OpenApiInfo { Title = "Jokes API", Version = "v1" });

           // use it if you want to hide Paths and Definitions from OpenApi documentation correctly
           //options.UseAllOfToExtendReferenceSchemas();


           var xmlFilePath = Path.Combine(AppContext.BaseDirectory, "JokesAPI.xml");
           options.IncludeXmlCommentsWithRemarks(xmlFilePath, false);

           // Add filters to fix enums
           // use by default:
           options.AddEnumsWithValuesFixFilters();
       });
builder.Services.AddScoped<IFetcherService, FetcherService>();
builder.Services.AddScoped<IJokeService, JokeService>();
builder.Services.AddScoped<IJokeCategoryService, JokeCategoryService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
