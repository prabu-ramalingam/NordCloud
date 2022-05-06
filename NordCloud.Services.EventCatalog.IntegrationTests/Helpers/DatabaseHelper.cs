using NordCloud.Services.EventCatalog.DbContexts;
using NordCloud.Services.EventCatalog.Entities;
using System;
using System.Linq;

namespace NordCloud.Services.EventCatalog.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        public static void InitialiseDbForTests(EventCatalogDbContext dbContext)
        {
            dbContext.Events.Add(new Event
            {
                EventId = Guid.NewGuid(),
                Date = DateTime.Now,
                Description = "Explore ways to improve your organisation's capabilities.",
                Format = "Workshop",
                Name = "Microsoft Azure Immersion Workshops"
            });

            dbContext.SaveChanges();
        }

        public static void ResetDbForTests(EventCatalogDbContext dbContext)
        {
            var events = dbContext.Events.ToArray();
            dbContext.Events.RemoveRange(events);
            dbContext.SaveChanges();
            InitialiseDbForTests(dbContext);
        }
    }
}
