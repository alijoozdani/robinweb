using Microsoft.AspNetCore.Http;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services.Interfaces
{
    public interface ISliderService
    {
        List<Slider> GetAllSliders();
        Slider GetSliderById(int sliderId);
        void AddSlider(Slider slider, IFormFile image);
        bool DeleteSlider(int sliderId);
    }
}
