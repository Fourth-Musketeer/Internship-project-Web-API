using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Task>> GetTasks();
        Task<Task> GetTask(int taskId);
        Task<Task> AddTask(Task task);
        Task<Task> UpdateTask(Task task);
        Task<Task> DeleteTask(int taskId);

    }
}
