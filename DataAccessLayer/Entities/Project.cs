
using WebAPIShared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Project
    {
        public int Id { get; set; }


        [Required]
        [StringLength(60, ErrorMessage = "Maximum number of characters for name is 60")]
        public string Name { get; set; }
        
        
        [DataType(DataType.DateTime,ErrorMessage ="Start date must be in date time format")]
        public DateTime? StartDate { get; set; }


        [DataType(DataType.DateTime, ErrorMessage = "Completion date must be in date time format")]
        public DateTime? CompletionDate { get; set; }

        [Required(ErrorMessage = "Current status is required")]
        public CurrentProjectStatus CurrentStatus { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        [Range(1, 10, ErrorMessage = "Project priority range is invalid (1-10)")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are alowed in field priority (1-10)")]
        public int Priority { get; set; }



    }

   
}