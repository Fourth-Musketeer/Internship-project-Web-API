﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
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
      public int Priority { get; set; }


      [Required(ErrorMessage = "Task status is required")]
      public TaskStatus TaskStatus  { get; set; }
      public Project Project { get; set; }   



    }
    public enum TaskStatus 
    { 
        ToDo, InProgress, Done 
    }
}
