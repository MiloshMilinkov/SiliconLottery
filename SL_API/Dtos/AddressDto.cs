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
        public string streetName { get; set; }
        [Required]
        public int streetNumber { get; set; }
        [Required]
        public string  city { get; set; }
        [Required]
        public string zipCode { get; set; }
    }
}