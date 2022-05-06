namespace NordCloud.Services.EventCatalog.Models
{
    public class EventDto
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
