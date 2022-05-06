using NordCloud.Gateway.WebBff.Models;

namespace NordCloud.Gateway.WebBff.Services
{
    public interface ICatalogService
    {

        Task<List<EventDto>> GetEvents();
        Task<EventDto> GetEventById(Guid eventId);
    }
}
