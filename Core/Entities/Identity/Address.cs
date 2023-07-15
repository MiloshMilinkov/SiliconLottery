using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string  City { get; set; }
        public string ZipCode { get; set; }

        //EntityFramework configuration
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
         
    }
}