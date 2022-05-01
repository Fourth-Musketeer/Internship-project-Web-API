using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public interface IProjectRepository 
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(int projectId);
        Task<Project> AddProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task<Project> DeleteProject(int projectId);

        

    }
}
