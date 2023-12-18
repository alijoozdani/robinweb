using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Pages.Admin.Sliders
{
    [Authorize]
    public class AddModel : PageModel
    {
        private ISliderService _sliderService;
        public AddModel(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        [BindProperty]
        public Slider Slider { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost(IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _sliderService.AddSlider(Slider, image);
            return RedirectToPage("Index");
        }
    }
}
