using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using NordCloud.Gateway.WebBff.Tests.Models;

namespace NordCloud.Gateway.WebBff.Tests.Controllers
{
    public class CatalogControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public CatalogControllerTests(WebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/bffweb/events/");
            _client = factory.CreateClient();
            _factory = factory;
        }

      
        public async Task GetEvents_ReturnsExpectedJson()
        {
            var model = await _client.GetFromJsonAsync<EventDtoTest>("");

            Assert.NotNull(model);            
        }
    
    }
}
