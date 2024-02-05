using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Application.StaticTools
{
    public class FilePath
    {
        //برای بارگذاری تصویر
        public static readonly string UploadImageProfile = "/images/user/profile/";

        //برای بارگذاری روی سرور
        public static readonly string UploadImageProfileServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/profile/");
    }
}
