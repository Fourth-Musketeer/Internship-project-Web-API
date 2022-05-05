using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIShared.Enums;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectServices
    {

         Task<Project> GetProject(int projectId);
         Task<IEnumerable<Project>> GetProjects();
         Task<Project> AddProject(ProjectModel project);
         Task<Project> DeleteProject(int projectId);
         Task<Project> UpdateProject(int ProjectId, ProjectModel project);
         Task<Project> UpdateProjectPatch(int projectId, JsonPatchDocument<Project> project);
         Task<IEnumerable<Project>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus, string sort);
         Task<IEnumerable<DataAccessLayer.Entities.Task>> FindAllTasks(int projectId);
         Task<Project> GetProjectByName(string name);
         Task<Project> GetProjectByNameAndId(string name, int id);


    }
}
