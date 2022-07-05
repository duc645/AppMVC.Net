using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cs68.Models;

namespace cs68.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string HighHome() =>"Xin chào các bạn,action HighHome ";

        public IActionResult Index()
        {
            //Controller có thể lấy đc thông tin của request gửi đến //
            //thông qua property this.HttpContext
            //Đọc được Http request : this.Request
            //Đọc đc this.Response
            //RouteDate ,....

            //Còn có this.User, this.ModelState, this.ViewData, this.ViewBag
            //this.Url, this,TempData
            
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
