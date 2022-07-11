using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using cs68.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cs68.Controllers {
    public class FirstController : Controller{
         private readonly ILogger<FirstController> _logger;
         private readonly ProductService _productService;

        public FirstController(ILogger<FirstController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public string Index(){
            
            _logger.LogInformation("Index action");
            //or
            Console.WriteLine("Action index Console");
            return "tôi là Index của FirstController";
        }
        public void Nothing(){
            _logger.LogInformation("Phuong thuc nothing");
            Response.Headers.Add("hi","xin chào các bạn");//trong đây mà viết hoa là lỗi
        }
        public object Anything() => new int[] {1,2,3,4,5} ;//Math.Sqrt(2); //DateTime.Now;

            // Kiểu trả về                 | Phương thức
            // ------------------------------------------------
            // ContentResult               | Content()
            // EmptyResult                 | new EmptyResult()
            // FileResult                  | File()
            // ForbidResult                | Forbid()
            // JsonResult                  | Json()
            // LocalRedirectResult         | LocalRedirect()
            // RedirectResult              | Redirect()
            // RedirectToActionResult      | RedirectToAction()
            // RedirectToPageResult        | RedirectToRoute()
            // RedirectToRouteResult       | RedirectToPage()
            // PartialViewResult           | PartialView()
            // ViewComponentResult         | ViewComponent()
            // StatusCodeResult            | StatusCode()
            // ViewResult                  | View()
        // cos @ thif xuoongs ddc nhieu dong
        // public IActionResult Readme(){
        //     return Content(@"abc

        //     day la van ban
        //     van ban 1
        //     ");
        // }
        public IActionResult Readme(){
            var content = @"abcnjcda


            
            day la tieu de ";
            return Content(content,"text/plain");//khi dat kieu tra ve la IActionResult 
            //framework chuyen noi dung nay thanh respon tra ve
        }

        public IActionResult Anh(){
            //Startup.ContentRootPath//duong dan den thu muc ung dung
            string filePath = Path.Combine(Startup.ContentRootPath, "Files" ,"cap12.PNG");
            var bytes = System.IO.File.ReadAllBytes(filePath);//MANG byte doc dc ti file png
            return File(bytes,"image/png");
        }

        public IActionResult IphonePrice(){
            //json co tham so la mot doi tuong 
            //no se convert doi tuong do thanh chuoi json
            return Json(
                new {
                    productName = "sam sung",
                    Price = 100
                }
            );
        }
        public IActionResult Privacy(){
            
            //tạo ra url bằng Url chuyển hướng đến Action Privacy của Controller Home
            var url = Url.Action("Privacy","Home");
            _logger.LogWarning("Chuyển hướng đến privacy home");

            //LocalRedirect dam bao tham so la url ,tức là ko có phần host
            return LocalRedirect(url);
        }

        public IActionResult Google(){
            var url = "https://google.com";
            _logger.LogInformation("Chuyển hướng đến privacy home" + url);

            //Redirect có thể chuyển hướng đến bất kì url nào , kể cả url ở ngoài
            //đia chỉ gg trên ko phải là địa chỉ local
            return Redirect(url);
        }
        //Một action có thể trả về ViewResult bằng cách gọi phương thức View() của Controller
        public IActionResult HelloView(string username){
            //muoons lay dc du lieu binding den , tren url, viet nhu sau
            //https://localhost:5001/First/HelloView?username=duc
            //co au ? ngan cach voi ten action
            if(string.IsNullOrEmpty(username)){
                username = "khách ";
            }
            
            //TH1 : file cshtml ko nawmf trong thu muc View
            //View() => Razor , đọc file .cshtml trả về cho ViewResult
            //tham số thứ nhất là cái file cshtml muốn render
            //tham số 2 là tham số truyền đến view 
            //Chú ý : đường dẫn tới view phải là đường dẫn tuyệt đối
            // return View("/MyView/xinchao1.cshtml",username);

            //TH2 : file cshtml nằm trong thư mục /View/tenController
            //tham số thứ nhất chỉ cần tên file cshtml 
            // return View("xinchao2",username);


            //TH3 :file cshtml trùng tên với action     
            //nó sẽ tìm HelloView.cshtml trong thư mục /View/First/HelloView.cshtml
            //chỉ cần return View() , ko cần tham số nó sẽ tự hiểu là 
            //file trùng tên action
            //THAM SỐ truyền thì phải convert sang object để nó ko hiểu là file cshtml
            //return View((object)username);

            //trường hợp file cshtml ko nằm trong /View/Controller/Action.cshtml
            //thì sẽ tìm trong : /MyView/Controller/Action.cshtml
            //đã được thiết lập trong file Startup
            return View("xinchao3",username);
        }
        

        //AcceptVerbs("POST") chi cho phep truy cap action = phuong thuc POST
        //truy cap bang phuong thuc GET se ko vao dc
        //neu muon dung phuong thuc khac de truy cap thi phai viet them vao
        [AcceptVerbs("POST","GET")]
        public IActionResult ViewProduct(int? id){
            //Where là phương thức của list
            //p => p.Id = id : lấy ra sản phẩm mà sản phẩm đó có Id = id
            //FirstOrDefault : lấy sản phẩm đầu tiên
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if(product == null){
                return NotFound();
            }
            //truyền dữ liệu sang view bằng cách sd Model
            //lúc này nó sẽ tìm file cshtml theo mặc định
            // /Views/First/ViewProduct.cshtml
            //return View(product);

            // this.ViewData["product"] = product;
            // ViewData["Title"] = product.Name;
            // return View("ViewProduct2");

            ViewBag.product = product;
            return View("ViewProduct3");
        }
        [TempData]
        public string StatusMessage {get;set;}

        public IActionResult ViewProduct_tempdata(int? id){
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if(product == null){

                //NGOÀI ra còn có thể cho TempData là một thuộc tính
                //StatusMessage tương đương  TempData["StatusMessage"]
                // TempData["StatusMessage"] = "San phẩm bạn yeu cầu ko có!";
                StatusMessage = "sản phẩm bạn yêu cầu ko có";
                return Redirect(Url.Action("Index","Home"));
            }
            
            ViewBag.product = product;
            return View("ViewProduct_tempdata");
        }
        

    }
}
//truyền dữ liệu
//c1 ://truyền dữ liệu sang view bằng cách sd Model,thiết lập model cho view

//c2 : truyền dữ liệu từ controller sang view bằng ViewData
//this.ViewData["key"] = "value";

//c3: truyền dữ liệu sang View sử dụng ViewBag
//gần giống ViewData , khác chỗ là có thể thiết lập thuộc tính ngay tại thời điểm thực thi
//vd : ViewBag.product = product


//truyền dữ liệu từ trang này qua trang khác
//vd : từ trang sản phẩm sang trang Home
//dùng TempData , section của hệ thống sẽ lưu dữ liệu này , và trang khách
//có thể đọc đc