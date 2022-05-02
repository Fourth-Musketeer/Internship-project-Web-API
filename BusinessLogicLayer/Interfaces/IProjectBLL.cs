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
         Task<Project> AddProject(Project project);
         Task<Project> DeleteProject(int projectId);
         Task<Project> UpdateProject(Project project);
         Task<Project> UpdateProjectPatch(int projectId, JsonPatchDocument<Project> project);
         Task<IEnumerable<Project>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus);
         Task<Project> GetProjectByName(string name);
       
      
    }
}
