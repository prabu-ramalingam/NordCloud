using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NordCloud.Integration.Messages;
using NordCloud.Integration.MessagingBus;
using NordCloud.Services.EventCatalog.Controllers;
using NordCloud.Services.EventCatalog.Entities;
using NordCloud.Services.EventCatalog.Models;
using NordCloud.Services.EventCatalog.Profiles;
using NordCloud.Services.EventCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NordCloud.Services.EventCatalog.Tests.Controllers
{
    public class EventControllerTests
    {

        private readonly Mock<IEventRepository> eventRepositoryMock;
        private readonly Mock<IMessageBus> messageBusMock;
        private readonly Mock<ILogger<EventController>> loggerMock;
        private readonly IMapper mapper;

        public EventControllerTests()
        {

            eventRepositoryMock = new Mock<IEventRepository>();
            messageBusMock = new Mock<IMessageBus>();
            loggerMock = new Mock<ILogger<EventController>>();


            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventProfile());
            });
            mapper = mockMapper.CreateMapper();

        }

        [Fact]

        public async Task Get_AllEvents_Success()
        {
            var fakeEvents = GetFakeEventsEntity();
            eventRepositoryMock.Setup(x => x.GetEventsAsync()).Returns(Task.FromResult(fakeEvents.AsEnumerable()));
            messageBusMock.Setup(x => x.PublishMessage(It.IsAny<MessageBase>(), It.IsAny<string>()));

            EventController eventController = new EventController(eventRepositoryMock.Object, mapper, messageBusMock.Object,  loggerMock.Object);

            var result = await eventController.Get();

            Assert.Equal((int)HttpStatusCode.OK, (result.Result as OkObjectResult).StatusCode);
            Assert.Equal(fakeEvents.Count,(((ObjectResult)result.Result).Value as List<EventDto>).Count);
        }

        [Fact]

        public async Task Post_Event_Success()
        {
            var fakeEvent = GetFakeEventsDto().FirstOrDefault();

            eventRepositoryMock.Setup(x => x.AddEvent(It.IsAny<Event>()));
            messageBusMock.Setup(x => x.PublishMessage(It.IsAny<MessageBase>(), It.IsAny<string>()));

            EventController eventController = new EventController(eventRepositoryMock.Object, mapper, messageBusMock.Object, loggerMock.Object);

            var result = await eventController.Post(fakeEvent);

            Assert.Equal((int)HttpStatusCode.Accepted, (result.Result as AcceptedResult).StatusCode);
            Assert.Equal(fakeEvent.EventId,(((ObjectResult)result.Result).Value as EventDto).EventId);
        }

        private List<Event> GetFakeEventsEntity()
        {
            return new List<Event>
            {
                new Event
                {
                    EventId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Description =  "Explore ways to improve your organisation's capabilities.",
                    Format="Workshop",
                    Name="Microsoft Azure Immersion Workshops"
                }
            };
        }
        private List<EventDto> GetFakeEventsDto()
        {
            return new List<EventDto>
            {
                new EventDto
                {
                    EventId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Description =  "Explore ways to improve your organisation's capabilities.",
                    Format="Workshop",
                    Name="Microsoft Azure Immersion Workshops"
                }
            };
        }
    }
}
