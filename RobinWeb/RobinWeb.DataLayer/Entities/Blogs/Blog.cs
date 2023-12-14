using System.ComponentModel.DataAnnotations;

namespace RobinWeb.DataLayer.Entities
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Display(Name = "عنوان بلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400)]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        public string ShortLink { get; set; }

        [Display(Name = "متن کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string BlogText { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string Tags { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        [Display(Name = "بازدید")]
        public int BlogVisit { get; set; }

        public string Blogger { get; set; }

        [Display(Name = "تاریخ ساخت")]
        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }

        #region NavigationProperty
        public int UserId { get; set; }
        public User? User { get; set; }
        #endregion
    }
}
