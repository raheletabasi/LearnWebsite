using LearnWebsite.Core.DTOs;
using LearnWebsite.Data.Entities.CashWallet;
using LearnWebsite.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearnWebsite.Core.DTOs.UserPanelViewModel;

namespace LearnWebsite.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel user);
        void UpdateUser(User user);
        bool CheckDuplicateUser(int userId, string userName);
        bool CheckDuplicateEmail(int userId, string email);
        bool AccountActivation(string activeCode);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string userName);
        UserInformationViewModel GetUserInformation(string userName);
        int GetUserIdByUserName(string userName);
        User GetUserByUserId(int userId);

        #region Profile
        SideBarViewModel GetSideBar(string User);      
        EditProfileViewModel GetInfoForEdit(string userName);
        void UpdateProfilePanel(string oldUserName, EditProfileViewModel profile);
        bool GetPassword(string userName, string oldPassword);
        void UpdatePassword(string userName, string newPassword);
        #endregion

        #region CashWallet
        int GetCashWalletBalanceUserId(string userName);
        IEnumerable<HistoryCashWalletViewModel> GetHistoryCashWallet(string userName);
        int ChargeCashWallet(string userName, decimal cash);
        int SaveCashWallet(CashWallet cashWallet);
        CashWallet GetWalletByWalletId(int walletId);
        void UpdateWallet(CashWallet wallet);
        #endregion

        #region ManagementUser
        ManagementUserViewModel GetUser(int page = 1, string filterEmail = "", string filterUserName = "");
        int AddUserInAdmin(CreateUserViewModel user);
        EditUserViewModel GetUserInfoInAdmin(int userId);
        void EditUserInAdmin(EditUserViewModel editProfileViewModel);
        

        #endregion

    }
}
