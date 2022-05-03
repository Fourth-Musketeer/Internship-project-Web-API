using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using System;
using BusinessLogicLayer;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System.Net;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly ITaskBLL _taskBLL;

        public TasksController(ITaskBLL taskBLL)
        {
            _taskBLL = taskBLL;
        }

        [HttpGet]
        public async Task<ActionResult> GetTasks()
        {

            try
            {
                return  Ok(await _taskBLL.GetTasks());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");

            }     
           
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<DataAccessLayer.Entities.Task>> GetTask(int id)
        {
            try
            {
                var result = await _taskBLL.GetTask(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }



        [HttpPost]
        public async Task<ActionResult<DataAccessLayer.Entities.Task>> CreateTask(TaskModel taskModel)
        {
            try
            {
                if (taskModel == null)
                    return BadRequest();

                var ProjectOfTask = _taskBLL.GetProject(taskModel.ProjectId);

                if (ProjectOfTask == null)
                {
                    return NotFound($"Project with Id = {taskModel.ProjectId} not found");
                }

                var createdTask = await _taskBLL.AddTask(taskModel);

                if (createdTask == null)
                {
                    ModelState.AddModelError("TaskStatus", "Task status error. If project is done status must be completed. If project is not started status cannot be completed");
                    return BadRequest(ModelState);
                }

                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new task record");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<DataAccessLayer.Entities.Task>> UpdateTask(int id, TaskModel taskModel)
        {
            try
            {
                var ProjectOfTask =  _taskBLL.GetProject(taskModel.ProjectId);

                var taskToUpdate = _taskBLL.GetTask(id).Result;///!!!

                if (ProjectOfTask == null)
                {
                    return NotFound($"Project with Id = {taskModel.ProjectId} not found");
                }
                else if (taskToUpdate == null)
                {
                    return NotFound($"Task with Id = {id} not found");
                }

                var UpdatedTask= await _taskBLL.UpdateTask(id, taskModel);
              
                if (UpdatedTask == null)
                {
                    ModelState.AddModelError("TaskStatus", "Task status error. If project is done status must be 'done'. If project is not started status must be 'toDo'");
                    return BadRequest(ModelState);
                }

                return  UpdatedTask;



            }
            catch (WebException)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating task record");
            }
            catch (Exception )
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating task record");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DataAccessLayer.Entities.Task>> DeleteTask(int id)
        {
            try
            {


                var taskToDelete = await _taskBLL.GetTask(id);

                if (taskToDelete == null)
                {
                    return NotFound($"Task with Id = {id} not found");
                }



                return Ok(await _taskBLL.DeleteTask(id));


            }
            catch (Exception )
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting task record");
            }
        }


    }
}
