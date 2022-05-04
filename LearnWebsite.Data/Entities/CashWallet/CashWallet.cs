using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LearnWebsite.Data.Entities.CashWallet
{
    public class CashWallet
    {
        public CashWallet()
        {

        }

        [Key]
        public int CashWalletId { get; set; }
        public int UserId { get; set; }

        public int CashTypeId { get; set; }

        [Display(Name = "مبلغ")]
        public decimal Cash { get; set; }

        [Display(Name = "تاریخ و ساعت ایجاد")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "پرداخت شده")]
        public bool IsPay { get; set; }


        public virtual User.User User { get; set; }
        public virtual CashType CashType { get; set; }
    }
}
