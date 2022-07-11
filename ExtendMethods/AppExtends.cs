using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AppExtendMethods {
    public static class AppExtend{
        public static void AddStatusCodePage(this IApplicationBuilder app){
            app.UseStatusCodePages(appError => {
                appError.Run(async context => {
                    var respone = context.Response;
                    var code = respone.StatusCode;

                    //lưu trữ thông tin về mã lỗi , ctrl + left click để xem
                    // (HttpStatusCode)code


                    var content = @$"<html>
                        <head>
                            <meta charset = 'UTF-8' />
                            <title>lỗi {code} </title>
                        </head>
                        <body>
                            <p style='color:red; font-size=30px'>
                            Có lỗi xảy ra : {code} - {(HttpStatusCode)code}
                            </p>
                        </body>
                    </html>";

                   await  respone.WriteAsync(content);
                });
            });
        }
    }
}