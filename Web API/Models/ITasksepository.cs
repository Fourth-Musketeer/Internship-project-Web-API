using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public interface ITasksepository
    {
        Task<IEnumerable<Task>> GetTasks();
        Task<Task> GetTask(int taskId);
        Task<Task> AddTask(Task task);
        Task<Task> UpdateTask(Task task);
        Task<Task> DeleteTask(int taskId);

    }
}
