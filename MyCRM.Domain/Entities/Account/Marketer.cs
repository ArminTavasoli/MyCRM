using MyCRM.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.Entities.Account
{
    public class Marketer
    {
        [Key , ForeignKey("User")]
        public long UserID { get; set; }

        [Display(Name ="رشته تحصیلی")]
        [MaxLength(20 , ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string FieldStudy { get; set; } = string.Empty;


        [Display(Name ="کد ملی")]
        [MaxLength(10 , ErrorMessage ="لطفا تعداد کارکتر خود را کم کنید")]
        public string IrCode { get; set; } =string.Empty;

        public int Age { get; set; }


        public Education MarketerEducation { get; set; }

        #region Relation
        public User User { get; set; }
        public ICollection<OrderSelectedMarketer> OrderSelectedMarketers { get; set; }
        #endregion 
    }


    public enum Education
    {
        [Display(Name ="دیپلم")]
        Diploma ,
        [Display(Name ="لیسانس")]
        Bachelor ,
        [Display(Name ="فوق لیسانس")]
        Master ,
        [Display(Name ="دکترا")]
        PHD
    }
}
