using System.ComponentModel.DataAnnotations;

namespace CoworkingReservation.Data.Models
{
    public class ReservationDelitionForAdmins
    {
        [Required]
        public string CoworkingName { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
    }
}
