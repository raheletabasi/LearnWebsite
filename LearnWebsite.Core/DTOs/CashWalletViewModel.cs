using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.DTOs
{
    public class ChargeCashWalletViewModel
    {
        [Display(Name ="مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public decimal Amount { get; set; }
    }

    public class HistoryCashWalletViewModel
    {
        public decimal Cash { get; set; }
        public int CashType { get; set; }
        public string Description { get; set; }
        public DateTime RegisterDateTime { get; set; }
    }
}
