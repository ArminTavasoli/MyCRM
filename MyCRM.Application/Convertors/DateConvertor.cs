using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Application.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsiDate(this DateTime time)
        {
            var persionCalender = new PersianCalendar();

            return persionCalender.GetYear(time) + "/" +
                   persionCalender.GetMonth(time).ToString("00") + "/" +
                   persionCalender.GetDayOfMonth(time).ToString("00");
        }
    }
}
