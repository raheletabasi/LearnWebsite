using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.DTOs
{
    public class UserPanelViewModel
    {
        public class UserInformationViewModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string RegisterDate { get; set; }
            public int CashWallet { get; set; }
        }

        public class SideBarViewModel
        {
            public string UserName { get; set; }
            public string RegisteDate { get; set; }
            public string Avatar { get; set; }
        }
    }
}
