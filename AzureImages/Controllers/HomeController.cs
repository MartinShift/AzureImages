using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureImages.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
