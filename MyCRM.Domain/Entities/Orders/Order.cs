﻿using MyCRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.Entities.Orders
{
    public class Order
    {
        #region Propertis

        [Key]
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

        public bool IsDelete { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool IsSale { get; set; } = false;

        public bool IsFinish { get; set; } = false;

        public long CustomerId { get; set; }


        [Display(Name = "نوع سفارش")]
        public OrderType OrderType { get; set; }

        #endregion

        #region Relations
        public Customer Customer { get; set; }  
        public OrderSelectedMarketer OrderSelectedMarketer { get; set; }
        #endregion
    }


    public enum OrderType
    {
        [Display(Name = "اعلامی")]
        Declaration,
        [Display(Name = "حضوری")]
        Presence,
        [Display(Name = "بازاریابی شده")]
        Marketing
    }
}
