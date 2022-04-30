using System;

namespace WebAPI.Shared
{
    public class Task
    {
      public int Id { get; set; }
      public string TaskName { get; set; }
      public string Description { get; set; }
      public int Priority { get; set; }
      public TaskStatus TaskStatus  { get; set; }
      public int ProjectId { get; set; }
      public Project Project { get; set; }   



    }
    public enum TaskStatus 
    { 
        ToDo, InProgress, Done 
    }
}
