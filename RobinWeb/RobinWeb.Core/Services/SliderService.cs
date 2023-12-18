using Microsoft.AspNetCore.Http;
using RobinWeb.Core.SaveAndDelete;
using RobinWeb.Core.Security;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Context;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services
{
    public class SliderService : ISliderService
    {
        private RobinWebDBContext _context;
        public SliderService(RobinWebDBContext context)
        {
            _context = context;
        }

        public void AddSlider(Slider slider, IFormFile image)
        {
            if (image != null && image.IsImage())
            {
                slider.ImageName = SaveFileInServer.SaveFile(image, "wwwroot/img/slider");
            }
            _context.Add(slider);
            _context.SaveChanges();
        }

        public bool DeleteSlider(int sliderId)
        {
            var slider = GetSliderById(sliderId);
            if (slider.ImageName != "nophoto.png" && slider.ImageName != null)
            {
                DeleteFileFromServer.DeleteFile(slider.ImageName, "wwwroot/img/slider");
            }
            _context.Remove(slider);
            _context.SaveChanges();
            return true;
        }

        public List<Slider> GetAllSliders()
        {
            return _context.SlidersTbl.ToList();
        }

        public Slider GetSliderById(int sliderId)
        {
            return _context.SlidersTbl.Find(sliderId);
        }
    }
}
