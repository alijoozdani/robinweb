using System.ComponentModel.DataAnnotations;

namespace RobinWeb.DataLayer.Entities
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? ImageName { get; set; }
        public string Link { get; set; }
    }
}
