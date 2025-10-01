using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoworkingReservation.Data.Models.DbModels
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CoworkingUnitId { get; set; }
        [Required]
        [JsonIgnore]
        public CoworkingUnit CoworkingUnit { get; set; }


        public DateTime StartTime { get; set; } = new();
        public DateTime EndTime { get; set; } = new();

        public Guid Guid { get; set; }
    }
}
