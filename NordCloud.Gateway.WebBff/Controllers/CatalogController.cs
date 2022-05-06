using Microsoft.AspNetCore.Mvc;
using NordCloud.Gateway.WebBff.Services;

namespace NordCloud.Gateway.WebBff.Controllers
{
    [ApiController]
    [Route("api/bffweb/events")]
    public class CatalogController : ControllerBase
    {

        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents(Guid categoryId)
        {
            var allevents = await catalogService.GetEvents();
            return Ok(allevents);
        }

    }
}
