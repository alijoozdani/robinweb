using System.ComponentModel.DataAnnotations;

namespace RobinWeb.DataLayer.Entities
{ 
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "عنوان بلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string ProductName { get; set; }

        [Display(Name = "توضیح کوتاه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400)]
        public string Comment { get; set; }

        [Display(Name = "شرح محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        public string ShortLink { get; set; }

        [Display(Name = "تصویر")]
        public string AvatarName { get; set; }

        [Display(Name = "دمو")]
        public string? DemoName { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int VisitCount { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }

    }
}
