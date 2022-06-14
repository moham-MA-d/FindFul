using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // this controller should serve ... files
    // it have to send requests for index.html so angular will be responsible for routes.
    public class FallbackController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"),
                     "text/HTML");
        }
    }
}