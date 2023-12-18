using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Web.Pages.Admin.ContactUs
{
    [Authorize]
    public class ShowModel : PageModel
    {
        private IContactUsService _service;

        public ShowModel(IContactUsService service)
        {
            _service = service;
        }

        public ContactUsForm ContactUs { get; set; }
        public void OnGet(int id)
        {
            ContactUs = _service.GetContactUsFormById(id);
            if (ContactUs == null)
            {
                Response.Redirect("/Admin/ContactUs");
            }
        }

        public IActionResult OnPost(int id, string answer)
        {
            ContactUs = _service.GetContactUsFormById(id);
            if (ContactUs == null)
            {
                Response.Redirect("/Admin/ContactUs");
            }
            if (string.IsNullOrEmpty(answer))
            {
                ModelState.AddModelError("answer", "پاسخ را وارد کنید");
                return Page();
            }

            _service.SendAnswerForContactUs(ContactUs, answer);
            return RedirectToPage("Index");
        }
    }
}
