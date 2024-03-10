using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.ViewModel.Account
{
    public class LoginUserViewModel
    {
        [Display(Name = "نام کاربری")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "کلمه عبور")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public enum LoginUserResult
    {
        Success,
        NotFound , 
        PasswordNotCorrect
    }
}
