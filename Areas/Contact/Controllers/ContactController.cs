using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cs68.models;
using ContactModel = cs68.models.Contacts.Contact;
using Microsoft.AspNetCore.Authorization;

namespace cs68.Areas.Contact.Controllers
{
    [Area("Contact")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Contact
        [HttpGet("admin/contact")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacts.ToListAsync());
        }

        // GET: Contact/Details/5
        [HttpGet("admin/contact/detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contact/Create
        [HttpGet("/contact/")]
        [AllowAnonymous]//muon truy cao vao day , user phai co vai tro gi do
        public IActionResult SendContact()
        {
            return View();
        }

        [TempData]
        public string StatusMessage {set;get;}

        // POST: Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/contact/")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendContact([Bind("FullName,Email,Message,Phone")] ContactModel contact)
        {
            //dùng Bind để chỉ ra một số properties cần bind ko cần binding tất cả
            //các properties , tùy ý thích của mình
            if (ModelState.IsValid)
            {
                contact.DateSent = DateTime.Now;
                _context.Add(contact);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index)) =>cùng Controller
                //còn đây là chuyển hướng về trang chủ
                StatusMessage = "Bạn đã gửi liên hệ thành công";
                return RedirectToAction("Index","Home");
            }
            return View(contact);
        }

        // // GET: Contact/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var contact = await _context.Contacts.FindAsync(id);
        //     if (contact == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(contact);
        // }

        // // POST: Contact/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,DateSent,Message,Phone")] ContactModel contact)
        // {
        //     if (id != contact.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(contact);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!ContactExists(contact.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(contact);
        // }

        // GET: Contact/Delete/5
        [HttpGet("admin/contact/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        //ActionName("Delete") thiết lập tên action DeleteConfirmed là Delete
        //có nghĩa là truy cập bằng pt post với action Delete thì nó sẽ 
        //vào action  DeleteConfirmed
        [HttpPost("admin/contact/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //kiem tra contact co ton tai ko
        // private bool ContactExists(int id)
        // {
        //     return _context.Contacts.Any(e => e.Id == id);
        // }
    }
}
