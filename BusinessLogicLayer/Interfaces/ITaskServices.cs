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
        Task<DataAccessLayer.Entities.Task> GetTask(int taskId);
        Task<IEnumerable<DataAccessLayer.Entities.Task>> GetTasks();
        Task<DataAccessLayer.Entities.Task> AddTask(TaskModel taskModel);
        Task<DataAccessLayer.Entities.Task> DeleteTask(Task task);
        Task<DataAccessLayer.Entities.Task> UpdateTask(int taskId, TaskModel taskModel);
        Project GetProject(int projectId);

    }
}
