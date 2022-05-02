using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Security;
using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Core.Utility.Convertor;
using LearnWebsite.Core.Utility.Generator;
using LearnWebsite.Data.Contexts;
using LearnWebsite.Data.Entities.User;
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

        public bool CheckDuplicateEmail(int userId, string email)
        {
            return _context.Users.Any(usr => usr.Email == email && usr.UserId != userId);
        }

        public bool CheckDuplicateUser(int userId, string userName)
        {
            return _context.Users.Any(usr => usr.UserName == userName && usr.UserId != userId);
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

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(usr => usr.ActivateCode == activeCode);
        }

        public User GetUserByEmail(string email)
        {
            var userEmail = FixedText.FixEmail(email);

            return _context.Users.SingleOrDefault(usr => usr.Email == userEmail);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(usr => usr.UserName == userName);
        }

        public UserInformationViewModel GetUserInformation(string userName)
        {
            User userInformation = GetUserByUserName(userName);

            UserInformationViewModel userInformationViewModel = new UserInformationViewModel()
            {
                UserName = userInformation.UserName,
                Email = userInformation.Email,
                RegisterDate = userInformation.RegisterDate.ToShamsi(),
                CashWallet = 0
            };

            return userInformationViewModel;
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
    }
}
