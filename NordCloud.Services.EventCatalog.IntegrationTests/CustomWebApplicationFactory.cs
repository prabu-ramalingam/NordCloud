using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NordCloud.Services.EventCatalog.DbContexts;
using NordCloud.Services.EventCatalog.IntegrationTests.Helpers;
using NordCloud.Services.EventCatalog.Repositories;
using System;
using System.Linq;

namespace NordCloud.Services.EventCatalog.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly InMemoryDatabaseRoot _dbRoot = new InMemoryDatabaseRoot();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Production");

            builder.ConfigureTestServices(services =>
            {
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                services.AddScoped<IEventRepository, EventRepository>();

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<EventCatalogDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<EventCatalogDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting", _dbRoot);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<EventCatalogDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        DatabaseHelper.InitialiseDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test data. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}
