using System.ComponentModel.DataAnnotations;

namespace CbtAdminPanel.Models
{
    public class TP_CityMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryID { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
    }
}
