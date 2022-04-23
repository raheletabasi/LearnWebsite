﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Data.Entities.User
{
    public class UserRole
    {
        public UserRole()
        {

        }

        public int UserId { get; set; }
        public int RoleId { get; set; }


        public virtual User User { get; set; }
        public virtual Role Role { get; set; }


    }
}
