using Microsoft.AspNetCore.Mvc;
using nvd_231230727_de02.Models;
using System.Diagnostics;

namespace nvd_231230727_de02.Controllers
{
    public class nvdHomeController : Controller
    {
        private readonly ILogger<nvdHomeController> _logger;

        public nvdHomeController(ILogger<nvdHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult nvdIndex()
        {
            return View();
        }

        public IActionResult nvdPrivacy()
        {
            return View();
        }

        public IActionResult nvdAbout()
        {
            ViewData["HoTen"] = "Ngô Vãn D?ng";
            ViewData["MSV"] = "231230727";
            ViewData["Lop"] = "CNTT2";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
