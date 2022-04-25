using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.Utility.Generator
{
    public class Generator
    {
        public static string CodeGenerator()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
