using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Globalization.Models;
using Globalization.Resources;

namespace Globalization.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.PageTitle = Resource.Index_Page_Title;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
