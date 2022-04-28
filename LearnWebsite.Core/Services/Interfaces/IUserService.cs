﻿using LearnWebsite.Core.DTOs;
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
        bool AccountActivation(string activeCode);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string userName);
        UserInformationViewModel GetUserInformation(string userName);
        SideBarViewModel GetSideBar(string User);       
    }
}
