using System.ComponentModel.DataAnnotations;

namespace CoworkingReservation.Data.Models
{
    public class CoworkingAddModel
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public int PricePerHour { get; set; }

        public List<IFormFile> Photos { get; set; } = new();

        public int MaxNumberOfPeople { get; set; }
        
        public bool IsLanCableAvailable { get; set; }
        
        public bool IsSoundProof { get; set; }

    }
}
