using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NordCloud.Integration.MessagingBus;
using NordCloud.Logging;
using NordCloud.Services.EventCatalog.DbContexts;
using NordCloud.Services.EventCatalog.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.Authority = "https://localhost:8080";
                       options.Audience = "eventcatalog";
                   });


var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
              .RequireAuthenticatedUser()
              .Build();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddDbContext<EventCatalogDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(configure =>
{
    //configure.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();


public partial class Program { }
