using System.ComponentModel.DataAnnotations;

namespace CoworkingReservation.Data.Models
{
    public class SearchModel
    {
        public string? Name { get; set; }
        public int? PricePerHour { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int? MaxNumberOfPeople { get; set; }
        
        public bool? IsLanCableAvailable { get; set; }
        
        public bool? IsSoundProof { get; set; }

    }
}
