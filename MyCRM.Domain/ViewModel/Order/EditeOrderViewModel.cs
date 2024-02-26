using MyCRM.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.ViewModel.Order
{
    public class EditeOrderViewModel
    {
        #region Order Propertis

        public long OrderId { get; set; }

        [Display(Name = "عنوان سفارش")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        [Required(ErrorMessage = "{0} اجباری است")]
        public string Description { get; set; } = string.Empty;

        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از بلاک {1} باشد")]
        public string ImageName { get; set; } = string.Empty;

        public long CustomerId { get; set; }


        [Display(Name = "نوع سفارش")]
        public OrderType OrderType { get; set; }

        #endregion
    }


    public enum EditeOrderResult
    {
        Success, 
        Fail
    }
}
