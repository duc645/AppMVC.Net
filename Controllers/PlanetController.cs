using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cs68.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cs68.Controllers
{
    [Route("he-mat-troi/[action]")]
    public class PlanetController : Controller
    {
        
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;

        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger = logger;
        }


        //neu co them dau / : la tuyet doi
        //thi ko can quan tram controller o phia trc
        [Route("/danh-sach-cac-hanh-tinh.html")]
        public IActionResult Index()
        {
            return View();
        }

        [BindProperty(SupportsGet =true , Name = "action")]
        //moi route deu co mot tham so la action
        public string Name {set;get;}
        //khi chung ta truy cap vao action thi thuoc tinh Name = tenaction

        [Route("abc")]
        public IActionResult Mercury()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail",planet);
        }
        public IActionResult Earth()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail",planet);
        }

        [HttpGet("/saomoc.html")]
        // [HttpPost("/saomoc.html")]
        public IActionResult Jupiter()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail",planet);
        }
        public IActionResult Mars()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail",planet);
        }
        public IActionResult Neptune()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail",planet);
        }

        public IActionResult Saturn()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail",planet);
        }

        public IActionResult Uranus()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail",planet);
        }

        [Route("sao/[action]", Order=1, Name = "venus1")]
        [Route("sao/[controller]/[action]",Order=2, Name = "venus2")]
        [Route("[controller]-[action].html",Order=3, Name = "venus3")]
        public IActionResult Venus()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail",planet);
        }

        [Route("hanhtinh/{id:int}")]
        public IActionResult PlanetInfo(int id)
        {
            var planet = _planetService.Where(p => p.Id == id).FirstOrDefault();
            return View("Detail",planet);
        }
    }
}