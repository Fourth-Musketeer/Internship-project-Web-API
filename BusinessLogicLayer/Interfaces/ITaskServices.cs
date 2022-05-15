using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = DataAccessLayer.Entities.Task;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITaskServices
    {
        Task<Task> GetTask(int taskId);
        Task<IEnumerable<Task>> GetTasks();
        Task<Task> AddTask(TaskModel taskModel);
        Task<Task> DeleteTask(Task task);
        Task<Task> UpdateTask(int taskId, TaskModel taskModel);
        Project GetProject(int projectId);

    }
}
