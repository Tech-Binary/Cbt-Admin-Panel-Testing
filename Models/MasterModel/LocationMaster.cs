using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CbtAdminPanel.Models.MasterModel
{
    public class LocationMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Locationprefix { get; set; }
        [Required]
        public string LocationId { get; set; }
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int City { get; set; }
        public int State { get; set; }
        public int Province { get; set; }
        [Required]
        public int Country { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModeifiedBy { get; set; }
        public DateTime LastModeifiedDate { get; set; }
        [NotMapped]

        public string CityName { get; set; }

        [NotMapped]

        public string CountryName { get; set; }

    }
}
