using DataAccessLayer.Entities;
using WebAPIShared.Enums;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProjectRepository 
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(int projectId);
        Task<Project> AddProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task<Project> DeleteProject(Project project);
        Task<Project> GetProjectByName(string name);
        Task<Project> GetProjectByNameAndId(string name, int id);
        Task<IEnumerable<Project>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus, string sort);
        Task<Project> UpdateProjectPatch(int projectId, JsonPatchDocument<Project> project);
        Task<IEnumerable<Entities.Task>> FindAllTasks(int projectId);







    }
}
