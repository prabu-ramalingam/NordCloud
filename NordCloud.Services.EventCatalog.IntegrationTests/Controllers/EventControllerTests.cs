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
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NordCloud.Services.EventCatalog.IntegrationTests.Controllers
{
    public class EventControllerTests: IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;   


        public EventControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/events/");
            _client = factory.CreateClient();
            _factory = factory;          

        }

        [Fact]
        public async Task GetEvents_ReturnsExpectedJson()
        {   
            var events = await _client.GetFromJsonAsync<List<EventDto>>("");         

            Assert.NotNull(events);
            Assert.True(events.Count > 0);
        }


    }
}
