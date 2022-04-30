using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace LearnWebsite.Core.Utility.Convertor
{
    public static class ConvertDate
    {
        public static string ToShamsi(this DateTime dateTime)
        { 
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(dateTime).ToString() + "/" +
                   persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(dateTime).ToString("00");
        }
    }
}
