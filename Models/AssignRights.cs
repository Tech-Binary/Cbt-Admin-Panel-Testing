using System.ComponentModel.DataAnnotations;

namespace CbtAdminPanel.Models
{
    public class AssignRights
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get;set; }
        public int ModifiedBy { get;set; }
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
    }
}
