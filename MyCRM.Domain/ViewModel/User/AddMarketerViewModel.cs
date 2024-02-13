using Microsoft.AspNetCore.Http;
using MyCRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.ViewModel.User
{
    public class AddMarketerViewModel
    {

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage ="نام کاربری را وارد کنید...")]
        [MaxLength(30, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string UserName { get; set; } = string.Empty;


        [Display(Name = "رمز")]
        [Required(ErrorMessage ="رمز را وارد کنید...")]
        [MaxLength(30, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
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


        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا شماره تلفن را وارد کنید...")]
        [MaxLength(11, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string MobilePhone { get; set; } = string.Empty;


        [Display(Name = "نام معرف")]
        [MaxLength(30, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string IntroduceName { get; set; } = string.Empty;


        [Display(Name = "رشته تحصیلی")]
        [MaxLength(20, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string FieldStudy { get; set; } = string.Empty;


        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string IrCode { get; set; } = string.Empty;

        public int Age { get; set; }


        public Education MarketerEducation { get; set; }

    }

    public enum AddMarketerResult
    {
        Success ,
        Fail
    }
}
