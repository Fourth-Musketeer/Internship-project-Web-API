using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
   

    
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext appDbContext;
        public TaskRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Entities.Task> AddTask(Entities.Task task)
        {
            if (task.Project != null)
            {
                appDbContext.Entry(task.Project).State = EntityState.Unchanged;
            }

            var result= await appDbContext.Tasks.AddAsync(task);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Entities.Task> DeleteTask(int taskId)
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

        public async Task<Entities.Task> GetTask(int taskId)
        {
            return await appDbContext.Tasks.Include(t=>t.Project).FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<IEnumerable<Entities.Task>> GetTasks()
        {
            return await appDbContext.Tasks.ToListAsync();
        }

        public async Task<Entities.Task> UpdateTask(Entities.Task task)
        {
            var result = await appDbContext.Tasks.FirstOrDefaultAsync(t=>t.Id==task.Id);

            result.ProjectId = task.ProjectId;
            result.Name = task.Name;
            result.Description=task.Description;
            result.TaskStatus = task.TaskStatus;
            result.Priority = task.Priority;
               
                
            await appDbContext.SaveChangesAsync();
        
            return result; 
            

          
        }

        public Project GetProject(int projectId)
        {
            var result=  appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            return result.Result;
        }
    }
}
