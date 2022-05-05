using DataAccessLayer.Entities;
using WebAPIShared.Enums;
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

        public readonly AppDbContext _appDbContext;

        public ProjectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;  
        }

        public async Task<Project> AddProject(Project project)
        {
            
            await _appDbContext.Projects.AddAsync(project);
            await _appDbContext.SaveChangesAsync();

            return project;
        }

        public async Task<Project> DeleteProject(Project project)
        {

                 _appDbContext.Projects.Remove(project);
                 await _appDbContext.SaveChangesAsync();

                return project;
            
        }

        public async Task<Project> GetProject(int projectId)
        {
            return await _appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _appDbContext.Projects.ToListAsync();
        }

        public async Task<Project> UpdateProject(Project project)
        {

           var result=await _appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);

                result.Name = project.Name;
                result.StartDate = project.StartDate;
                result.CompletionDate=project.CompletionDate;
                result.Priority=project.Priority;
                result.CurrentStatus=project.CurrentStatus;

                await _appDbContext.SaveChangesAsync();
                return result;
           
        }

        public  async Task<Project> GetProjectByName(string name)
        {
            return await _appDbContext.Projects.FirstOrDefaultAsync(p => p.Name == name);
            
           
        }
       public async Task<Project> GetProjectByNameAndId(string name, int id)
        {
            return  await _appDbContext.Projects.FirstOrDefaultAsync(p => p.Name == name && p.Id != id);
          
        }

        public  Task<IEnumerable<Project>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus, string sort)
        {
            //IQueryable<Project> query = _appDbContext.Projects;

            //if (!string.IsNullOrEmpty(name))
            //{
            //    query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            //}
            //if (currentProjectStatus != null)
            //{
            //    query = query.Where(p => p.CurrentStatus == currentProjectStatus);
            //}
            //if (priority != 0)
            //{
            //    query = query.Where(p => p.Priority == priority);
            //}
            //if (sort == "asc")
            //{
            //    query = query.OrderBy(p => p.Priority);
            //}


            //return await query.ToListAsync();


            throw  new System.NotImplementedException();// logic moved to ProjectServices


        }

        public async Task<IEnumerable<Entities.Task>> FindAllTasks(int projectId)
        {
            IQueryable<Entities.Task> AllTasksInProject = _appDbContext.Tasks.Where(t=>t.ProjectId == projectId);

            return await AllTasksInProject.ToListAsync();
        }


        public async Task<Project> UpdateProjectPatch(int projectId, JsonPatchDocument<Project> project)
        {
            var result = await _appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

                project.ApplyTo(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            
           
        }

       
    }
}
