﻿using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Data.Contexts;
using LearnWebsite.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.Services
{
    public class UserService : IUserService
    {
        LearnWebsiteContext _context;

        public UserService(LearnWebsiteContext learnWebsiteContext)
        {
            _context = learnWebsiteContext;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserId;
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(usr => usr.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(usr => usr.UserName == userName);
        }
    }
}
