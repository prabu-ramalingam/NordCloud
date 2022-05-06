using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NordCloud.Integration.MessagingBus;
using NordCloud.Services.EventCatalog.Messages;
using NordCloud.Services.EventCatalog.Repositories;
using System.Net;

namespace NordCloud.Services.EventCatalog.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;
        private readonly ILogger<EventController> logger;

        public EventController(IEventRepository eventRepository, IMapper mapper, IMessageBus messageBus, ILogger<EventController> logger)
        {
            this.eventRepository = eventRepository;
            this.mapper = mapper;
            this.messageBus = messageBus;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.EventDto>>> Get()
        {
            var evnets = await eventRepository.GetEventsAsync();
            return Ok(mapper.Map<List<Models.EventDto>>(evnets));
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<IEnumerable<Models.EventDto>>> GetById(Guid eventId)
        {
            var evnets = await eventRepository.GetEventByIdAsync(eventId);
            return Ok(mapper.Map<Models.EventDto>(evnets));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Models.EventDto>> Post(Models.EventDto @event)
        {
            try
            {
                using var scope = logger.BeginScope("Handling request for event {@event}", @event);

                var eventEntity = mapper.Map<Entities.Event>(@event);

                eventRepository.AddEvent(eventEntity);
                await eventRepository.SaveChangesAsync();

                var eventToReturn = mapper.Map<Models.EventDto>(eventEntity);

                EventMessage eventToMessage = mapper.Map<EventMessage>(eventEntity);

                //TODO
                try
                {
                    await messageBus.PublishMessage(eventToMessage, "eventcreated");
                }
                catch (Exception)
                {
                   throw;
                }

                return Accepted(eventToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);
            }

        }

    }
}
