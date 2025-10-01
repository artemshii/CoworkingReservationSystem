using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoworkingReservation.Data.Models.DbModels
{
    public class CoworkingPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]

        public int CoworkingUnitId { get; set; }
        [Required]
        [JsonIgnore]
        public CoworkingUnit CoworkingUnit { get; set; }
        [Required]
        public string? Url { get; set; }
    }
}
