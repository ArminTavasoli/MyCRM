using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.ViewModel.Order
{
    public class OrderSelectMarketerViewModel
    {
        public long OrderId { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string Description { get; set; } = string.Empty;

        public long MarketerId { get; set; }
    }

    public enum AddOrderSelectMarketerResult
    {
        Success , 
        Fail , 
        SelectMarketerExist
    }
}
