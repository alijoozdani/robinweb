using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Pages.Admin.Sliders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private ISliderService _sliderService;
        public IndexModel(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public List<Slider> Sliders { get; set; }
        public void OnGet()
        {
            Sliders = _sliderService.GetAllSliders();
        }
        public IActionResult OnGetDeleteSlider(int slideId)
        {
            var res = _sliderService.DeleteSlider(slideId);
            return res ? Content("Deleted") : Content("Error");
        }
    }
}
