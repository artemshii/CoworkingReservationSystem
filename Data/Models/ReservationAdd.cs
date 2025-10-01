using System.ComponentModel.DataAnnotations;

namespace CoworkingReservation.Data.Models
{
    public class ReservationAdd
    {
        [Required]
        public string CoworkingName { get; set; }
        [Required]
        public DateTime StartTime { get; set; } = new();
        [Required]
        public DateTime EndTime { get; set; } = new();
        

    }
}
