using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer
{
    public class TaskBLL : ITaskBLL
    {
        private readonly ITaskRepository _taskRepository;
        public TaskBLL(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;   
        }

        public async Task<DataAccessLayer.Entities.Task> GetTask(int taskId)
        {
            ///check existance
            return await _taskRepository.GetTask(taskId);
        }

        public async Task<IEnumerable<DataAccessLayer.Entities.Task>> GetTasks()
        {
            return await _taskRepository.GetTasks();
        }

        public async Task<DataAccessLayer.Entities.Task> AddTask(DataAccessLayer.Entities.Task task)
        {

            var result = await _taskRepository.AddTask(task);
            return result;
        }

        public async Task<DataAccessLayer.Entities.Task> DeleteTask(int taskId)
        {
            var result = await _taskRepository.GetTask(taskId);

            if (result != null)
            {
                await _taskRepository.DeleteTask(taskId);
                return result;
            }

            return null;

        }


        public async Task<DataAccessLayer.Entities.Task> UpdateTask(DataAccessLayer.Entities.Task task)
        {
            var result = await _taskRepository.GetTask(task.Id);

            if (result != null)
            {
                result = await _taskRepository.UpdateTask(task);
                return result;
            }

            return null;
        }

    }
}
