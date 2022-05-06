using NordCloud.Services.EventCatalog.Entities;

namespace NordCloud.Services.EventCatalog.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task<bool> SaveChangesAsync();
        void AddEvent(Event @event);
    }
}
