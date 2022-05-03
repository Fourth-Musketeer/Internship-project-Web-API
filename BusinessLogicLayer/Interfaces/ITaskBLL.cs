using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITaskBLL
    {
        Task<DataAccessLayer.Entities.Task> GetTask(int taskId);
        Task<IEnumerable<DataAccessLayer.Entities.Task>> GetTasks();
        Task<DataAccessLayer.Entities.Task> AddTask(TaskModel taskModel);
        Task<DataAccessLayer.Entities.Task> DeleteTask(int taskId);
        Task<DataAccessLayer.Entities.Task> UpdateTask(int taskId, TaskModel taskModel);
        Project GetProject(int projectId);

    }
}
