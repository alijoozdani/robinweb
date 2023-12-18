using Microsoft.AspNetCore.Mvc;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Entities;
using RobinWeb.Models;
using System.Diagnostics;

namespace RobinWeb.Controllers
{
    public class HomeController : Controller
    {
        private IContactUsService _contactUsService;
        public HomeController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/img/CKImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }

            var url = $"{"/img/CKImages/"}{fileName}";

            return Json(new { uploaded = true, url });
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUsForm contactUsForm)
        {
            if (!ModelState.IsValid)
                return View("Index");

            contactUsForm.CreateDate = DateTime.Now;
            _contactUsService.InsertQuestion(contactUsForm);
            TempData["Success"] = true;
            return Redirect("/Home");
        }

        [Route("/Home/HandleError/{code}")]
        public IActionResult HandlerError(int code)
        {


            if (code >= 500)
            {
                return View("ServerError");
            }

            return View("NotFound");

        }
    }
}