using NordCloud.Integration.Messages;

namespace NordCloud.Services.EventCatalog.Messages
{
    public class EventMessage:MessageBase
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
