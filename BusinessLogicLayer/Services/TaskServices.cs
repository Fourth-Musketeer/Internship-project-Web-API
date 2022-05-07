using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using WebAPIShared.Enums;

namespace BusinessLogicLayer
{
    public class TaskServices : ITaskServices
    {
        private readonly ITaskRepository _taskRepository;
        
        public TaskServices(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;   
        }

        public async Task<DataAccessLayer.Entities.Task> GetTask(int taskId)
        {

            return await _taskRepository.GetTask(taskId);
            
        }

        public async Task<IEnumerable<DataAccessLayer.Entities.Task>> GetTasks()
        {
            return await _taskRepository.GetTasks();
        }

        public async Task<DataAccessLayer.Entities.Task> AddTask(TaskModel taskModel)
        {


            var project = _taskRepository.GetProject(taskModel.ProjectId);

            if (project.StartDate == null && (taskModel.TaskStatus == CurrentTaskStatus.Done || taskModel.TaskStatus == CurrentTaskStatus.InProgress))
            {
                return null;
            }
            else if (project.CompletionDate.HasValue && (taskModel.TaskStatus == CurrentTaskStatus.ToDo || taskModel.TaskStatus == CurrentTaskStatus.InProgress))
            {
                return null;
            }
            else if (!Enum.IsDefined(typeof(CurrentTaskStatus), taskModel.TaskStatus))
            {
                return null;
            }

            DataAccessLayer.Entities.Task taskEntity = new DataAccessLayer.Entities.Task
            { 
                Name=taskModel.Name,
                ProjectId=taskModel.ProjectId,
                Description=taskModel.Description,
                Priority=taskModel.Priority,
                Project=project      

            };

           

            taskEntity.TaskStatus = taskModel.TaskStatus;

            var result = await _taskRepository.AddTask(taskEntity);
            return result;
                
        }

        public async Task<DataAccessLayer.Entities.Task> DeleteTask(DataAccessLayer.Entities.Task task)
        {
    
                await _taskRepository.DeleteTask(task);
                return task;

        }


        public async Task<DataAccessLayer.Entities.Task> UpdateTask(int taskId,TaskModel taskModel)
        {
           
            var project = _taskRepository.GetProject(taskModel.ProjectId);


            if (project.StartDate == null && (taskModel.TaskStatus == CurrentTaskStatus.Done || taskModel.TaskStatus == CurrentTaskStatus.InProgress))
            {
                return null;
            }
            else if (project.CompletionDate.HasValue && (taskModel.TaskStatus == CurrentTaskStatus.ToDo || taskModel.TaskStatus == CurrentTaskStatus.InProgress))
            {
                return null;
            }
            else if (!Enum.IsDefined(typeof(CurrentTaskStatus), taskModel.TaskStatus))
            {
                return null;
            }


            DataAccessLayer.Entities.Task taskEntity = new DataAccessLayer.Entities.Task
            {
                Id = taskId,
                Name = taskModel.Name,
                ProjectId = taskModel.ProjectId,
                Description = taskModel.Description,
                Priority = taskModel.Priority,
                Project = project
            };



            taskEntity.TaskStatus = taskModel.TaskStatus;

            var result = await _taskRepository.UpdateTask(taskEntity);
            return result;
        }

       public Project GetProject(int projectId)
        {
            return  _taskRepository.GetProject(projectId);
        }

    }
}
