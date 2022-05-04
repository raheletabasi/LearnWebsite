using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Data.Entities.CashWallet
{
    public class CashType
    {
        public CashType()
        {

        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CashTypeId { get; set; }

        [Required]
        [Display(Name = "عنوان نوع")]
        [MaxLength(100,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string TypeTitle { get; set; }

        public virtual IEnumerable<CashWallet> CashWallets { get; set; }
    }
}
