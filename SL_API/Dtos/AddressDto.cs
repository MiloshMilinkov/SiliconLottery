using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SL_API.Dtos
{
    public class AddressDto
    {
        [Required]
        public string StreetName { get; set; }
        [Required]
        public int StreetNumber { get; set; }
        [Required]
        public string  City { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}