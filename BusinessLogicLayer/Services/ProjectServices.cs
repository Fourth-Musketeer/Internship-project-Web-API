using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using WebAPIShared.Enums;
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
    public class ProjectServices : IProjectServices
    {

        private readonly IProjectRepository _projectRepository;

        public ProjectServices(IProjectRepository projectRepository)
        {
           _projectRepository=projectRepository;
        }



        public async Task<Project> GetProject(int projectId)
        {
           
            return await _projectRepository.GetProject(projectId);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            

            var projects = await _projectRepository.GetProjects();

            if (projects == null)
            {
                return null;
            }

            return projects;

        }


        public async Task<Project> AddProject(ProjectModel project)
        {

            Project projectEntity = new Project   
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

            
                await _projectRepository.DeleteProject(result);
                return result;
            

          

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


        public async Task<IEnumerable<Project>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus, string sort)
        {
            IEnumerable<Project> query = await _projectRepository.GetProjects();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            if (currentProjectStatus != null)
            {
                query = query.Where(p => p.CurrentStatus == currentProjectStatus);
            }
            if (priority != 0)
            {
                query = query.Where(p => p.Priority == priority);
            }
            if (sort == "asc")
            {
                query = query.OrderBy(p => p.Priority);
            }

            //  return await _projectRepository.Search( name,  priority,  currentProjectStatus,  sort);
            return query;

            }

        public async Task<IEnumerable<DataAccessLayer.Entities.Task>> FindAllTasks(int projectId)
        {
            return await _projectRepository.FindAllTasks(projectId);
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
