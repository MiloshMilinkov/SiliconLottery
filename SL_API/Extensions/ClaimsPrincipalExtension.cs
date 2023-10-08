using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SL_API.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string RetriveEmailFromClaimpPrincipal(this ClaimsPrincipal user){
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}