using Microsoft.AspNetCore.Mvc;

namespace PLP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Playlists()
        {
            ViewData["Message"] = "Current Available Playlists";

            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "All About Playlist Pro!";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Reach out to me!";

            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
