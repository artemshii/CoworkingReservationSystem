using System.ComponentModel.DataAnnotations;

namespace CoworkingReservation.Data.Models
{
    public class ReservationDelition
    {
        [Required]
        public string CoworkingName { get; set; }
        [Required]

        public Guid Guid { get; set; }
    }
}
