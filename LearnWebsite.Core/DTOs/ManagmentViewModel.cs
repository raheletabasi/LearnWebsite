using LearnWebsite.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.DTOs
{
    public class ManagementUserViewModel
    {
        public List<User> Users { get; set; }

        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
