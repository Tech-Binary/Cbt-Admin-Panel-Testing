﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CbtAdminPanel.Models.MasterModel
{
    public class ModuleMaster
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public string? ModuleName { get; set; }
        public string? ModuleDescription { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string? ProjectName { get; set; }


        [NotMapped]
        public string? UserName { get; set; }
        [NotMapped]
        public bool  IsView { get; set; }
        [NotMapped]
        public bool  IsEdit { get; set; }
    }
}
