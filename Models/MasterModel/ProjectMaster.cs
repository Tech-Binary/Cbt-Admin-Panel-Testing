using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CbtAdminPanel.Models.MasterModel
{
    public class ProjectMaster
    {
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public int LocationId { get; set; }

        public DateTime ProjectDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string LocationName { get; set; }
    }
}
