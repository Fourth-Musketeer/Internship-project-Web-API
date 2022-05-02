
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Entities.Task>> GetTasks();
        Task<Entities.Task> GetTask(int taskId);
        Task<Entities.Task> AddTask(Entities.Task task);
        Task<Entities.Task> UpdateTask(Entities.Task task);
        Task<Entities.Task> DeleteTask(int taskId);

    }
}
