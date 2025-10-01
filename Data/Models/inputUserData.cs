using System.ComponentModel.DataAnnotations;

namespace CoworkingReservation.Data.Models
{
    public class inputUserData 
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

       
    }
}
