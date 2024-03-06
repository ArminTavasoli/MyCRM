using MyCRM.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Application.Extensions
{
    public static class UserExtension
    {
        //User Show Name
        public static string GetUserShowName(this User user)
        {
            if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
            {
                return $"{user.FirstName} {user.LastName}";
            }

            return user.MobilePhone;
        }


        //Get UserId
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if(claimsPrincipal == null)
            {
                var data = claimsPrincipal.Claims.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier);
                
                if(data != null)
                {
                    return Convert.ToInt64(data?.Value);
                }
                return 0;
            }

            return 0;
        }
    }

}
