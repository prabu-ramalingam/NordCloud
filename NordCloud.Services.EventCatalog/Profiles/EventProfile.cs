using AutoMapper;

namespace NordCloud.Services.EventCatalog.Profiles
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            CreateMap<Entities.Event,Models.EventDto>().ReverseMap();
            CreateMap<Entities.Event,Messages.EventMessage>().ReverseMap();
        }

    }
}
    