using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SL_API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression
            ("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[\\W_]).{6,}$"
            , ErrorMessage ="Password must have at least 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters."
            )]
        public string Password { get; set; }
    }
}