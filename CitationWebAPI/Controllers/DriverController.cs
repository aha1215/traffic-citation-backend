using Microsoft.AspNetCore.Mvc;

namespace CitationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<String> HelloWorld()
        {
            return new string[] {"Hello", "World"};
        }
    }
}