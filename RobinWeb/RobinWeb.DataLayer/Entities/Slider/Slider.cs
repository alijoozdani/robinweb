using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinWeb.DataLayer.Entities.Slider
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ImageName { get; set; }
        public string Link { get; set; }
    }
}
