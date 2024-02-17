using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Application.StaticTools
{
    public class FilePath
    {
        #region Users
        //تصویر پیش فرض
        public static readonly string UserProfileDefault = "/images/user/default/avatar.png";

        //برای بارگذاری تصویر
        public static readonly string UploadImageProfile = "/images/user/profile/";

        //برای بارگذاری روی سرور
        public static readonly string UploadImageProfileServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/profile/");
        #endregion

        #region Orders
        public static readonly string OrderImagePath = "/images/order/image/";
        public static readonly string OrderImagePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/order/image/");
        #endregion
    }
}
