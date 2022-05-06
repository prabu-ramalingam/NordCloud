using Microsoft.AspNetCore.Authentication.JwtBearer;
using NordCloud.Gateway.WebBff.DelegatingHandlers;
using NordCloud.Gateway.WebBff.Services;
using NordCloud.Gateway.WebBff.Url;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var configuration=builder.Configuration;

// Add services to the container.


var authenticationScheme = "";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(authenticationScheme, options =>
       {
           options.Authority = "https://localhost:8081";
           options.Audience = "nordcloudgateway";
       });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<ServiceUrls>(configuration.GetSection("ServiceUrls"));
builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
          c.BaseAddress = new Uri(configuration["ServiceUrls:EventCatalog"]));

builder.Services.AddScoped<TokenExchangeDelegatingHandler>();

builder.Services.AddOcelot()
    .AddDelegatingHandler<TokenExchangeDelegatingHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

public partial class Program { }
