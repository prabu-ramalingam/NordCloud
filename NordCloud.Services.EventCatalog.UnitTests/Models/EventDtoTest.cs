using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCloud.Gateway.WebBff.Tests.Models
{
      public class EventDtoTest
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
