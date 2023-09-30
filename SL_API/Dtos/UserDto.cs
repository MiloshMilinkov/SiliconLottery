using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL_API.Dtos
{
    public class UserDto
    {
        public string email { get; set; }
        public string displayName { get; set; }
        public string token { get; set; }
    }
}