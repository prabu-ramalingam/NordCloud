using System.ComponentModel.DataAnnotations;

namespace NordCloud.Services.EventCatalog.Entities
{
    public class Event
    {
        [Key]
        public Guid EventId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Format { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
