using Microsoft.EntityFrameworkCore;
using NordCloud.Services.EventCatalog.Entities;

namespace NordCloud.Services.EventCatalog.DbContexts
{
    public class EventCatalogDbContext:DbContext
    {
        public EventCatalogDbContext(DbContextOptions<EventCatalogDbContext> options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }

    }
}
