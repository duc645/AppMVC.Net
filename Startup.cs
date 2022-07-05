using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cs68.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();//để truy cập đến những trang razor ta cần đky
            });
        }
    }
}
//khi ung dung dang chay nhan ctrl + c để dừng