namespace NordCloud.Gateway.WebBff.Url
{
    public class EventCatalogOperations
    {
        public static string GetAllEvents() => $"/api/events";
        public static string GetEventById(Guid id) => $"/api/events/{id}";
    }
}
