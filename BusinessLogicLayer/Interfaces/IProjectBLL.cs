using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectBLL
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
