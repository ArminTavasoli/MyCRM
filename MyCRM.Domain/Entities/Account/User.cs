using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.Entities.Account
{
    public class User
    {
        [Key]
        public long UserID { get; set; }

        [Display(Name = "نام کاربری")]
        [MaxLength(30, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string UserName { get; set; } = string.Empty;


        [Display(Name = "رمز")]
        [MaxLength(100, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string Password { get; set; } = string.Empty;


        [Display(Name = "نام")]
        [MaxLength(30, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string FirstName { get; set; } = string.Empty;


        [Display(Name = "نام خانوادگی")]
        [MaxLength(30, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string LastName { get; set; } = string.Empty;


        [Display(Name = "ایمیل")]
        [MaxLength(30, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string Email { get; set; } = string.Empty;


        [Display(Name = "نام تصویر")]
        [MaxLength(100, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string ImageName { get; set; } = string.Empty;


        [Display(Name = "شماره تلفن")]
        [MaxLength(11, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string MobilePhone { get; set; } = string.Empty;


        [Display(Name = "نام معرف")]
        [MaxLength(30, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string IntroduceName { get; set; } = string.Empty;

        public Gender UserGender { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }


        #region Relation
        public Marketer Marketer { get; set; } 

        public Customer Customer  { get; set; }

        #endregion Relation
    }

    public enum Gender
    {
        [Display(Name = "عمومی")]
        General,
        [Display(Name = "مرد")]
        Male,
        [Display(Name = "خانم")]
        Femail
    }
}
