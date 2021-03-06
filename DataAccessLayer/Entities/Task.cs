using WebAPIShared.Enums;
using System;
using System.ComponentModel.DataAnnotations;


namespace DataAccessLayer.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Maximum number of characters for name is 60")]
        public string Name { get; set; }


        [StringLength(600, ErrorMessage = "Maximum number of characters for description is 600")]
        public string Description { get; set; }


        [Required(ErrorMessage = "priority is required")]
        [Range(1, 10, ErrorMessage = "Task priority range is invalid (1-10)")]
        public int Priority { get; set; }

       
        [Required(ErrorMessage = "Task status is required")]
        public CurrentTaskStatus TaskStatus { get; set; }
        public Project Project { get; set; }



    }
   
}
