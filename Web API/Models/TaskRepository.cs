using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_API.Data;

namespace Web_API.Models
{
   

    
    public class TaskRepository : ITasksepository
    {
        private readonly AppDbContext appDbContext;
        public TaskRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Task> AddTask(Task task)
        {
            if (task.Project != null)
            {
                appDbContext.Entry(task.Project).State = EntityState.Unchanged;
            }

            var result= await appDbContext.Tasks.AddAsync(task);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Task> DeleteTask(int taskId)
        {
            var result = await appDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

           if (result != null)
            {
                appDbContext.Tasks.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;

        }

        public async Task<Task> GetTask(int taskId)
        {
            return await appDbContext.Tasks.Include(t=>t.Project).FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<IEnumerable<Task>> GetTasks()
        {
            return await appDbContext.Tasks.ToListAsync();
        }

        public async Task<Task> UpdateTask(Task task)
        {
            var result = await appDbContext.Tasks.FirstOrDefaultAsync(t=>t.Id==task.Id);

            if (result != null)
            {
                result.Priority = task.Priority;
                result.TaskStatus = task.TaskStatus;
                result.Name=task.Name;
                result.Description = task.Description;
                if (task.ProjectId != 0)
                {
                    result.ProjectId = task.ProjectId;
                }
                else if (task.Project != null)
                {
                    result.ProjectId = task.Project.Id;
                }
               
                await appDbContext.SaveChangesAsync();
                return result; 
            }

            return null;
        }
    }
}
