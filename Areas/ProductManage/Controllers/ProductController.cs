using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cs68.Services;

namespace cs68.Controllers
{
    [Area("ProductManage")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
         private readonly ProductService _productService;

        public ProductController(ILogger<ProductController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [Route("/cac-san-pham/{id?}")]
        public IActionResult Index()
        {

            var products = _productService.OrderBy(p => p.Name).ToList();
            //file cshtml lưu tại , /Areas/areaName/Views/Controller/Action.cshtml
            //trong TH này : Areas/ProductManage/Views/Product/Index.cshtml
            return View(products);
        }
    }
}