using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AppExtendMethods;
using cs68.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace cs68
{
    public class Startup
    {
        public static string ContentRootPath {set;get;}
        public Startup(IConfiguration configuration , IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //co cai nay moi tao ra dc cai endpoints MVC, tiep do moi truy cap dc cac trang mvc
            services.AddControllersWithViews();
            services.AddRazorPages();//đky phương thức 
            //để có thể tạo ra url di chuyển đến các trang razor

            services.Configure<RazorViewEngineOptions>(options =>{
                //mặc định : /View/Controller/Action.cshtml
                //bây giờ thiết lập nếu tìm trong đường dẫn trên ko thấy 
                //thì tiếp tục tìm
                // /MyView/Controller/Action.cshtml
                //{0} -> ten Action
                //{1} -> ten Controller
                //{2} -> ten Area
                
                //bằng cách thiết lập như trên
                //khi ko tìm thấy file cshtml : trong /View/Controller/Action.cshtml
                //nó sẽ tiếp tục tìm trong : /MyView/Controller/Action.cshtml
                options.ViewLocationFormats.Add("/MyView/{1}/{0}"+ RazorViewEngine.ViewExtension);
            } );

            // services.AddSingleton<ProductService>();
            // services.AddSingleton<ProductService,ProductService>();
            // services.AddSingleton(typeof(ProductService));
            services.AddSingleton(typeof(ProductService), typeof(ProductService));
            services.AddSingleton<PlanetService>();//co nhieu cach de dky




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // app.UseStatusCodePages(); la mot midlleware , khi ung dung xay ra loi tu 400 den 599
            //tao ra noi dung response tu loi 400 tro di
            app.AddStatusCodePage();//tuyf bien loi tu 400 - 599

            app.UseRouting();

            app.UseAuthorization();//xac thuc quyen truy cap 
            app.UseAuthentication();//xác thực danh tính (đăng nhập)

            app.UseEndpoints(endpoints =>
            {
                // MVC có thể truy cập các trang razor(như các bài trước) hay là các controller ,action
                //có phương thức MapControllerRoute, MapRazorPage

                //URL : /{controller}/{action}/{id?}
                //nó sẽ khởi tạo ra controller theo tên, sau đố chuyển request cho controler
                //và thi hành cái phương thức action 

                //nếu ko có controler thì mặc định là HomeController, ko có action
                //thì mặc định là index
                //ko có controler : vd :https://localhost:5001/
                //ko có action : vd : https://localhost:5001/Home
                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller=Home}/{action=Index}/{id?}");

                // endpoints.MapRazorPages();
                //để truy cập đến những trang razor ta cần đky


                //nếu viết như này thì nó đã tạo ra một điểm truy cập endpoints
                //dịa chỉ truy cập : /sayhi
                // endpoints.MapGet("/sayhi",async(context) => {

                // });

                endpoints.MapGet("/sayhi",async(context) => {
                    //viết ra dognf thông báo
                    await context.Response.WriteAsync($"hello asp mvc {DateTime.Now}");
                });
                //co cai nay moi truy cap dc nhung trang razorpage
                //endpoints.MapRazorPages();


                //khi dang ki  services.AddControllersWithViews(); o ConfigureServices roi thi co the tao rA
                //CO THE ANH xa url vao cac controler, cac diem endpoints co them phuong thuc de xay dung
                //ra cai anh xa url
                //endpoints.MapControllers, 
                //endpoints.MapControllerRoute 
                    //=> khi dung endpoints.MapControllers, endpoints.MapControllerRoute 
                        //thì trong các action của controller đc phép dùng [Route]
                        //để tạo ra các route
                //endpoints.MapDefaultControllerRoute
                //endpoints.MapAreaControllerRoute
                //4 phuong thuc
                //MapControllers la cau hinh tao ra cac endpoint toi nhung controler
                

                //dung atribute de tao route
                //[AcceptVerbs]
                //[Route]
                //[HttpGet]
                //[HttpPost]
                //[HttpPut], [HttpDelete],[HttpHead],[HttpPatch],..




                endpoints.MapControllerRoute(
                    name: "first",
                    //pattern: "xemsanpham/{id?}"
                    //neu thay xemsanpham = {url}
                    //thi truy cap với đường dẫn bất kì, vd : nfvnjsvns/3 thì nó vẫn sẽ vào 
                    //First/ViewProduct/3
                    pattern: "{url}/{id:range(2,4)}",
                    defaults: new {
                        controller = "First",
                        action = "ViewProduct"
                    },
                    //constraints :thiết lập ràng buộc cho tham số,url
                    constraints: new {
                        //tham so la bieu thuc chinh quy
                        //url = new RegexRouteConstrain()
                        
                        //ràng buộc url phai có xemsanpham
                        // url = new StringRouteConstraint("xemsanpham"),


                        //thiet lap id phai nam trong tu 2 -> 4
                        //thiet lap nay co the viet tren pattern cx dc
                        // id = new RangeRouteConstraint(2,4)
                    }

                );



                endpoints.MapAreaControllerRoute (
                    name: "product",
                    //chỗ này bỏ đi controller mặc đinh
                    pattern : "/{controller}/{action=Index}/{id?}",
                    areaName: "ProductManage"

                );

                //phuong thuc nay tham so 1 :ten route ,tham so 2 : mau url phu hop 
                //tham so 3 : cac tham so cua url , tham so 4 : thiet lap rang buoc
                //tham so 3, 4 co the null nhung ung dung se ko biet la controller nao no se anh xa
                endpoints.MapControllerRoute (
                    name : "default",
                    // khi truy cap url ten start-here no se goi FirstController, vao action ViewProduct 
                    //va truyen id = 3
                    //url , ko truyen vao id thi mac dinh id = 3
                    pattern : "/{controller=Home}/{action=Index}/{id?}",//co 2 url phu hop o day : start-here hoac start-here/1
                    defaults: new {
                        //neu ma bo di tham so thi cac truong tren bat buoc phai nhap du
                        // controller = "First",
                        // action = "ViewProduct",
                        // id = 3
                    }
                );



            });
        }
    }
}
//khi ung dung dang chay nhan ctrl + c để dừng

// <img class="card-image-top" src="/planet/@(Model.Name).jpg" alt="">
// @(Model.Name).jpg

//atribute [Route] taoj ra url anhs xa vao action 
//hay la tao ra url den cac action do 
//vd : [Route("danh-sach-cac-hanh-tinh.html")]
//Co the thiet lap tham so cho [Route] :[Route("hanhtinh/{id:int}")]
//khi thiet lap thuoc tinh [Route] thi cac endponit.mapControl... bi vo hieu hoa
//action nao ma ko co atribute thi van su dung endpoint default nhu bt
//Co the them vao cac tham so khac vao atribute va dat trong dau ngoac vuong
//[controler] [action] [area]
//vd : [Route("sao/[action]")] => /sao/Venus thi moi truy cap
//[Route("sao/[controller]/[action]")]
//[Route("[controller]-[action].html")]
//chu y : [Route] co the khai bao nhieu lan , 3 cai tren deu dung dc cung luc
//them OrDer de the hien do uu tien giua cac route , va cung co the dat ten Route
//[Route("sao/[action]", Order=1)
 // [Route("sao/[controller]/[action]",Order=2)]
 // [Route("[controller]-[action].html",Order=3)]
 //        [Route("[controller]-[action].html",Order=3, Name = "venus3")]
 //KHI o trong view dung : @Url.RouteUrl("ten_route") la tao dc ra link den action do


 //atribute [Route] ko những thiết lập đc ở action mà còn ở controller
 //khi thiet lap Route cho Controller thi tat ca cac action ben trong cung phai thiet lap
 //neu ko se loi
 //Nếu action mà ko muốn thì thiết lập , thì thêm token vào [Route] của controller
 //vd: [Route("he-mat-troi/[action]")]

 //HttpGet giống [Route], cũng thiết lập địa chỉ truy cập .Khác này khống chỉ truy cập
 //bằng phương thức Get

 //lenh tao controler
 //=> dotnet aspnet-codegenerator controller -name Product -namespace cs68.Controllers -outDir Controllers

 //thiết lập thuộc tính Area cho một controller
 //[Area("ProductManage")]

 //endpoints.MapControllerRoute chỉ thực hiện đc trên controller ko có Area
 //ko ánh xạ đc url vào controller có Area
 //=> Dùng endpoints.MapAreaControllerRoute
 //Lệnh tạo area : dotnet aspnet-codegenerator area areaName


 //Url.Action
 //Url.ActionLink