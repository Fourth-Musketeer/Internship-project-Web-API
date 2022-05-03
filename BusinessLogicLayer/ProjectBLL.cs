using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
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


        public async Task<Project> AddProject(ProjectModel project)
        {

            Project projectEntity = new Project  //automapper 
            { 
              Name=project.Name,
              StartDate=project.StartDate,
              CompletionDate=project.CompletionDate,
              Priority=project.Priority,
              CurrentStatus=CurrentProjectStatus.NotStarted
            };

            if (project.StartDate != null && project.CompletionDate == null)
            {
                projectEntity.CurrentStatus = CurrentProjectStatus.Active;
            }
            else if (project.StartDate != null && project.CompletionDate != null)
            {
                projectEntity.CurrentStatus = CurrentProjectStatus.Completed;
            }
           
            var result = await _projectRepository.AddProject(projectEntity);

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

        public async Task<Project> UpdateProject(int ProjectId,ProjectModel project)
        {
            

            Project projectEntity = new Project  
            {
                Id = ProjectId,
                Name = project.Name,
                StartDate = project.StartDate,
                CompletionDate = project.CompletionDate,
                Priority = project.Priority,
                
            };

            if (project.StartDate != null && project.CompletionDate == null)
            {
                projectEntity.CurrentStatus = CurrentProjectStatus.Active;
            }
            else if (project.StartDate != null && project.CompletionDate != null)
            {
                projectEntity.CurrentStatus = CurrentProjectStatus.Completed;
            }
            else
            {
                projectEntity.CurrentStatus = CurrentProjectStatus.NotStarted;
            }

           var result = await _projectRepository.UpdateProject(projectEntity);

            return result;

            
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

       public async Task<Project> GetProjectByNameAndId(string name, int id)
        {
            var result = await _projectRepository.GetProjectByNameAndId(name, id);
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
