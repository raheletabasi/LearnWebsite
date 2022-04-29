﻿using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Security;
using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Core.Utility.Convertor;
using LearnWebsite.Core.Utility.Generator;
using LearnWebsite.Data.Contexts;
using LearnWebsite.Data.Entities.User;
using System;
using System.Collections.Generic;
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

        public SideBarViewModel GetSideBar(string User)
        {
            return _context.Users.Where(usr => usr.UserName == User).Select(usr => 
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

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
