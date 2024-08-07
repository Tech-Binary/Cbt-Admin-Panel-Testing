using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CbtAdminPanel.Models.MasterModel.MasterSeries
{
    public class MasterSeries
    {
    }


    public class LocationSeries
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? LocName { get; set; }

        public DateTime CreatedDate { get; set; }
        public int Createdby { get; set; }

        public bool Active { get; set; }

        [NotMapped]
        public string? UserName { get; set; }

    }
}
