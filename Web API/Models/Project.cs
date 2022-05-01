
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Web_API.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]

        [StringLength(60, ErrorMessage = "Maximum number of characters for description is 60")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        [Required(ErrorMessage = "Current status is required")]
        public CurrentStatus CurrentStatus { get; set; }
        [Required(ErrorMessage = "Priority is required")]
        [Range(1, 10, ErrorMessage = "Range is invalid (1-10)")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are alowed in priority (1-10)")]
        public int Priority { get; set; }



    }

   
}