using Microsoft.AspNetCore.Mvc;

namespace DisasterAlleviation.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
