using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ProjectBLL : IProjectBLL
    {

        private readonly IProjectRepository _projectRepository;

        public ProjectBLL(IProjectRepository projectRepository)
        {
           _projectRepository=projectRepository;
        }



        public async Task<Project> GetProject(int projectId)
        {
            ///check existance
            return await _projectRepository.GetProject(projectId);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectRepository.GetProjects();
        }


        public async Task<Project> AddProject(Project project)
        {
            var result = await _projectRepository.AddProject(project);
            return result;

        }


        public async Task<Project> DeleteProject(int projectId)
        {
            var result = await _projectRepository.GetProject(projectId);

            if (result != null)
            {
                await _projectRepository.DeleteProject(result);
                return result;
            }

            return null;

        }

        public async Task<Project> UpdateProject(Project project)
        {
            var result = await _projectRepository.GetProject(project.Id);

            if (result != null)
            {
                await _projectRepository.UpdateProject(project);
                return result;
            }

            return null;
        }


        public async Task<Project> UpdateProjectPatch(int projectId, JsonPatchDocument<Project> project)
        {
            var result = await _projectRepository.GetProject(projectId);

            if (result != null)
            {
               await _projectRepository.UpdateProjectPatch(projectId, project);
                return result;
            }

            return null;
        }


        public async Task<IEnumerable<Project>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus)
        {

            return await _projectRepository.Search( name,  priority,  currentProjectStatus);

        }


        public async Task<Project> GetProjectByName(string name)
        {
            var result =await _projectRepository.GetProjectByName(name);
            return result;

        }

        //public bool ValidateDate(DateTime begin, DateTime? end)
        //{
        //    if (begin > end)
        //        return true;

        //    return false;
        //}

    }
}
