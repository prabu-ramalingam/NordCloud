using Microsoft.EntityFrameworkCore;
using NordCloud.Services.EventCatalog.DbContexts;
using NordCloud.Services.EventCatalog.Entities;

namespace NordCloud.Services.EventCatalog.Repositories
{
    public class EventRepository:IEventRepository
    {
        private readonly EventCatalogDbContext eventCatalogDbContext;

        public EventRepository(EventCatalogDbContext eventCatalogDbContext)
        {
            this.eventCatalogDbContext = eventCatalogDbContext;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await eventCatalogDbContext.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await eventCatalogDbContext.Events
                .Where(e => e.EventId == eventId)
                .FirstOrDefaultAsync();
        }

        public void AddEvent(Event @event)
        {
           eventCatalogDbContext.Events.Add(@event);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await eventCatalogDbContext.SaveChangesAsync() > 0;
        }
    }
}
