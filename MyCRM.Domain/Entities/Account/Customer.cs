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
    public class Customer
    {
        [Key , ForeignKey("User")]
        public long UserID { get; set; }

        [Display(Name = "شغل")]
        [MaxLength(30, ErrorMessage ="لطفا تعداد کارکتر خود را کم کنید")]
        public string Job { get; set; } = string.Empty;


        [Display(Name ="نام شرکت")]
        [MaxLength (30 , ErrorMessage = "لطفا تعداد کارکتر خود را کم کنید")]
        public string CompanyName { get; set; } = string.Empty;

        #region Relation
        public User User { get; set; }
        public ICollection<Order> OrderCollection { get; set; }
        #endregion Relation
    }
}
