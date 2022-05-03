using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
   

    
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;
        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Entities.Task> AddTask(Entities.Task task)
        {
            if (task.Project != null)
            {
                _appDbContext.Entry(task.Project).State = EntityState.Unchanged;
            }

            var result= await _appDbContext.Tasks.AddAsync(task);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Entities.Task> DeleteTask(int taskId)
        {
            var result = await _appDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

           if (result != null)
            {
                _appDbContext.Tasks.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }

            return null;

        }

        public async Task<Entities.Task> GetTask(int taskId)
        {
            return await _appDbContext.Tasks.Include(t=>t.Project).FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<IEnumerable<Entities.Task>> GetTasks()
        {
            return await _appDbContext.Tasks.ToListAsync();
        }

        public async Task<Entities.Task> UpdateTask(Entities.Task task)
        {
            var result = await _appDbContext.Tasks.FirstOrDefaultAsync(t=>t.Id==task.Id);

            result.ProjectId = task.ProjectId;
            result.Name = task.Name;
            result.Description=task.Description;
            result.TaskStatus = task.TaskStatus;
            result.Priority = task.Priority;
               
                
            await _appDbContext.SaveChangesAsync();
        
            return result; 
            

          
        }

        public Project GetProject(int projectId)
        {
            var result= _appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            return result.Result;
        }
    }
}
