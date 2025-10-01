using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoworkingReservation.Data.Models.DbModels
{
    
    public class CoworkingUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PricePerHour { get; set; }

        public CoworkingFlags CoworkingFlags { get; set; }
        public List<Reservation> Reservations { get; set; } = new();
        public List<CoworkingPhoto> CoworkingPhotos { get; set; } = new();

    }
}
