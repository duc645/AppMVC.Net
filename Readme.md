## Controller
-Là môt lớp kế thừa từ lớp Controller :Microsoft.AspNetCore.Mvc.Controller
-Action trong controller là một phương thức public (ko đc khai báo static)
-Action trả về bất kì kiểu dữ liệu nào, thường là IActionResult
-Các dịch vụ inject vào controller qua hàm tạo
## View
-là file cshtml
-View cho Action mặc định lưu tại : /View/ControllerName/ActionName.cshtml
-Thêm thư mục lữu trữ view, đã ghi chú kĩ trong file startup

## Truyền dữ liệu sang View
-Model(đối tượng product)
-ViewData
-ViewBag
-TempData