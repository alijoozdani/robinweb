using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.DTOs.ContactUs;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Web.Pages.Admin.ContactUs
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IContactUsService _contact;

        public IndexModel(IContactUsService contact)
        {
            _contact = contact;
        }
        public ContactUsFormViewModel ContactUsModel { get; set; }
        public void OnGet(int pageId = 1, bool isPosted = false)
        {
            ContactUsModel = _contact.GetContactUses(pageId, 12, isPosted);
        }
    }
}
