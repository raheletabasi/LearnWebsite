using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Security;
using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Core.Utility.Convertor;
using LearnWebsite.Core.Utility.Generator;
using LearnWebsite.Data.Contexts;
using LearnWebsite.Data.Entities.CashWallet;
using LearnWebsite.Data.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearnWebsite.Core.DTOs.UserPanelViewModel;

namespace LearnWebsite.Core.Services
{
    public class UserService : IUserService
    {
        LearnWebsiteContext _context;

        public UserService(LearnWebsiteContext learnWebsiteContext)
        {
            _context = learnWebsiteContext;
        }

        public bool AccountActivation(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(usr => usr.ActivateCode == activeCode);
            
            if(user != null || (user != null && user.IsActive))
            {
                user.IsActive = true;
                user.ActivateCode = Generator.CodeGenerator();

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserId;
        }

        public int AddUserInAdmin(CreateUserViewModel user)
        {
            User newUser = new User()
            {
                IsActive = true,
                ActivateCode = Generator.CodeGenerator(),
                Email = user.Email,
                Password = PasswordHelper.EncodePasswordMd5(user.Password),
                RegisterDate = DateTime.Now,
                UserName = user.UserName,
            };

            if (user.Avatar != null)
            {
                string imagePath = string.Empty;

                newUser.UserAvatar = Generator.CodeGenerator() + Path.GetExtension(user.Avatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar/" + newUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.Avatar.CopyTo(stream);
                }
            }
            _context.Users.Add(newUser);    

            return newUser.UserId;
        }

        public int ChargeCashWallet(string userName, decimal cash)
        {
            var cashWallet = new CashWallet()
            {
                CashTypeId = 1,
                CreateDate = DateTime.Now,
                UserId = GetUserIdByUserName(userName),
                IsPay = false,
                Cash = cash
            };

            return SaveCashWallet(cashWallet);
        }

        public bool CheckDuplicateEmail(int userId, string email)
        {
            return _context.Users.Any(usr => usr.Email == email && usr.UserId != userId);
        }

        public bool CheckDuplicateUser(int userId, string userName)
        {
            return _context.Users.Any(usr => usr.UserName == userName && usr.UserId != userId);
        }

        public void DeleteUserInAdmin(int userId)
        {
            User user = _context.Users.Find(userId);
            user.IsDelete = true;

            UpdateUser(user);
        }

        public void EditUserInAdmin(EditUserViewModel editProfileViewModel)
        {
            User currentUser = GetUserByUserId(editProfileViewModel.UserId);

            currentUser.Email = editProfileViewModel.Email;

            if (!String.IsNullOrEmpty(editProfileViewModel.Password))
                currentUser.Password = PasswordHelper.EncodePasswordMd5(editProfileViewModel.Password);

            currentUser.IsActive = editProfileViewModel.IsActive;

            if (editProfileViewModel.Avatar != null)
            {
                //Delete old Image
                if (editProfileViewModel.UserAvatar != "Defult.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editProfileViewModel.UserAvatar);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                currentUser.UserAvatar = Generator.CodeGenerator() + Path.GetExtension(editProfileViewModel.Avatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", currentUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editProfileViewModel.Avatar.CopyTo(stream);
                }
            }

            _context.Users.Update(currentUser);
            _context.SaveChanges();

        }

        public int GetCashWalletBalanceUserId(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            // اعتبار
            var credit = _context.CashWallets
                        .Where(w => w.UserId == userId
                                 && w.CashTypeId == 1
                                 && w.IsPay == true)
                        .Select(w => w.Cash);

            // بدهکار
            var debit = _context.CashWallets
                        .Where(w => w.UserId == userId
                                 && w.CashTypeId == 2)
                        .Select(w => w.Cash);

            return ((int)(credit.Sum() - debit.Sum()));
        }

        public IEnumerable<HistoryCashWalletViewModel> GetHistoryCashWallet(string userName)
        {
            int userId = GetUserIdByUserName(userName);
            return _context.CashWallets.Where(cw => cw.UserId == userId)
                                       .Select(cw => 
                                       new HistoryCashWalletViewModel()
                                       { 
                                            Cash = cw.Cash,
                                            CashType = cw.CashTypeId,
                                            RegisterDateTime = cw.CreateDate
                                       });
        }

        public EditProfileViewModel GetInfoForEdit(string userName)
        {
            return _context.Users.Where(usr => usr.UserName == userName)
                .Select(usr =>
                new EditProfileViewModel()
                {
                    UserName = usr.UserName,
                    Email = usr.Email,
                    AvatarName = usr.UserAvatar
                }).Single();
        }

        public bool GetPassword(string userName, string oldPassword)
        {
            string oldPassMD5 = Security.PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users.Any(usr => usr.UserName == userName && usr.Password == oldPassMD5 );
        }

        public SideBarViewModel GetSideBar(string User)
        {
            return _context.Users.Where(usr => usr.UserName == User)
                .Select(usr => 
                new SideBarViewModel() 
                {  
                    UserName = usr.UserName,
                    RegisteDate = usr.RegisterDate.ToShamsi(),
                    Avatar = usr.UserAvatar
                }).Single();               
        }

        public ManagementUserViewModel GetUser(int page = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterEmail))
                result = result.Where(usr => usr.Email.Contains(filterEmail));

            if (!string.IsNullOrEmpty(filterUserName))
                result = result.Where(usr => usr.UserName.Contains(filterUserName));

            // pageing
            int take = 1;
            int skip = (page - 1) * take;

            ManagementUserViewModel listOfUser = new ManagementUserViewModel()
            {
                Users = result.OrderBy(usr => usr.RegisterDate).Skip(skip).Take(take).ToList(),
                CurrentPage = page,
                PageCount = result.Count() / take
            };

            return listOfUser;
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(usr => usr.ActivateCode == activeCode);
        }

        public User GetUserByEmail(string email)
        {
            var userEmail = FixedText.FixEmail(email);

            return _context.Users.SingleOrDefault(usr => usr.Email == userEmail);
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(usr => usr.UserName == userName);
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public EditUserViewModel GetUserInfoInAdmin(int userId)
        {
            User UserInfo = GetUserByUserId(userId);

            EditUserViewModel editUserViewModel = new EditUserViewModel()
            { 
                UserId = userId,
                Email = UserInfo.Email,
                IsActive = UserInfo.IsActive,
                UserAvatar = UserInfo.UserAvatar,
                Roles = _context.UserRoles
                                .Select(rol => rol.RoleId).ToList(),                                                
            };

            return editUserViewModel;
        }

        public UserInformationViewModel GetUserInformation(string userName)
        {
            User userInformation = GetUserByUserName(userName);

            UserInformationViewModel userInformationViewModel = new UserInformationViewModel()
            {
                UserName = userInformation.UserName,
                Email = userInformation.Email,
                RegisterDate = userInformation.RegisterDate.ToShamsi(),
                CashWallet = GetCashWalletBalanceUserId(userName)
            };

            return userInformationViewModel;
        }

        public UserInformationViewModel GetUserInformation(int useId)
        {
            User userInformation = GetUserByUserId(useId);

            UserInformationViewModel userInformationViewModel = new UserInformationViewModel()
            {
                UserName = userInformation.UserName,
                Email = userInformation.Email,
                RegisterDate = userInformation.RegisterDate.ToShamsi(),
                CashWallet = GetCashWalletBalanceUserId(userInformation.UserName)
            };

            return userInformationViewModel;
        }

        public CashWallet GetWalletByWalletId(int walletId)
        {
            return _context.CashWallets.Find(walletId);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(usr => usr.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(usr => usr.UserName == userName);
        }

        public User LoginUser(LoginViewModel user)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(user.Password);
            string email = FixedText.FixEmail(user.Email);

            return _context.Users.SingleOrDefault(usr => usr.Email == email && usr.Password == hashPassword);
        }

        public int SaveCashWallet(CashWallet cashWallet)
        {
            _context.CashWallets.Add(cashWallet);
            _context.SaveChanges();

            return cashWallet.CashWalletId;
        }

        public void UpdatePassword(string userName, string newPassword)
        {
            var currentUser = GetUserByUserName(userName);
            currentUser.Password = PasswordHelper.EncodePasswordMd5(currentUser.Password);
            UpdateUser(currentUser);
        }

        public void UpdateProfilePanel(string oldUserName, EditProfileViewModel profile)
        {
            if (profile.AvatarUploaded != null)
            {
                string imagePath = string.Empty;
                if (profile.AvatarName != "Default.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar/" + profile.AvatarName);
                    if (File.Exists(imagePath))
                        File.Delete(imagePath);
                }
                profile.AvatarName = Generator.CodeGenerator() + Path.GetExtension(profile.AvatarUploaded.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar/" + profile.AvatarName) ;
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.AvatarUploaded.CopyTo(stream);
                }
            }

            var user = GetUserByUserName(oldUserName);
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatarName;

            UpdateUser(user);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void UpdateWallet(CashWallet wallet)
        {
            _context.CashWallets.Update(wallet);
        }
    }
}
