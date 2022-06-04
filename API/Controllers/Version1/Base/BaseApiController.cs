using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Version1.Base
{
    [Produces("application/json")]
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}