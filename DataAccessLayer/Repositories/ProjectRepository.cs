using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace DataAccessLayer.Repositories
{
    public class ProjectRepository : IProjectRepository
    {

        public readonly AppDbContext appDbContext;
        public ProjectRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;  
        }
        public async Task<Project> AddProject(Project project)
        {
            //var result = await appDbContext.Projects.AddAsync(project);
           await appDbContext.Projects.AddAsync(project);
            await appDbContext.SaveChangesAsync();

            //return result.Entity;
            return project;
        }

        public async Task<Project> DeleteProject(Project project)
        {
            //var result = await appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

                appDbContext.Projects.Remove(project);
                 await appDbContext.SaveChangesAsync();
                return project;
          

          
           
            
        }

        public async Task<Project> GetProject(int projectId)
        {
            return await appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await appDbContext.Projects.ToListAsync();
        }

        public async Task<Project> UpdateProject(Project project)
        {

           var result=await appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);

                result.Name = project.Name;
                result.StartDate = project.StartDate;
                result.CompletionDate=project.CompletionDate;
                result.Priority=project.Priority;
                result.CurrentStatus=project.CurrentStatus;

                await appDbContext.SaveChangesAsync();
                return result;

           
        }

        public  async Task<Project> GetProjectByName(string name)
        {
            var result= await appDbContext.Projects.FirstOrDefaultAsync(p => p.Name == name);
            return result;
           
        }

        public async Task<IEnumerable<Project>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus)
        {
            IQueryable<Project> query = appDbContext.Projects;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            if (priority!=0)
            {
                query = query.Where(p => p.Priority == priority);
            }
            if (currentProjectStatus != null)
            {
                query = query.Where(p => p.CurrentStatus == currentProjectStatus);
            }

            return await query.ToListAsync();

        
        }

      
          public async Task<Project> UpdateProjectPatch(int projectId, JsonPatchDocument<Project> project)
        {
            var result = await appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

                project.ApplyTo(result);
                await appDbContext.SaveChangesAsync();
                return result;
            

           
        }
    }
}
