using NordCloud.Gateway.WebBff.Extensions;
using NordCloud.Gateway.WebBff.Models;
using NordCloud.Gateway.WebBff.Url;

namespace NordCloud.Gateway.WebBff.Services
{
    public class CatalogService: ICatalogService
    {
        private readonly HttpClient client;

        public CatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<EventDto> GetEventById(Guid eventId)
        {

            var response = await client.GetAsync(EventCatalogOperations.GetEventById(eventId));
            return await response.ReadContentAs<EventDto>();

        }

        public async Task<List<EventDto>> GetEvents()
        {
            var response = await client.GetAsync(EventCatalogOperations.GetAllEvents());
            return await response.ReadContentAs<List<EventDto>>();
        }
    }
}
