namespace CbtAdminPanel.Models
{
    public class FormPages
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string? Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
