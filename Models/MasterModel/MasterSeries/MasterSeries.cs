using System.ComponentModel.DataAnnotations;

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
        public string LocName { get; set; }

        public DateTime CreatedDate { get; set; }
        public int Createdby { get; set; }

        public bool Active { get; set; }

    }
}
