using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json.Serialization;

namespace CoworkingReservation.Data.Models.DbModels
{
    public class CoworkingFlags
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CoworkingUnitId { get; set; }
        [Required]
        [JsonIgnore]
        public CoworkingUnit? CoworkingUnit { get; set; }
        [Required]

        public int MaxNumberOfPeople { get; set; }
        [Required]
        public bool IsLanCableAvailable { get; set; }
        [Required]
        public bool IsSoundProof { get; set; }
        


    }
}
