using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using System;
using BusinessLogicLayer;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Interfaces;

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
        public async Task<ActionResult<DataAccessLayer.Entities.Task>> CreateTaskt(DataAccessLayer.Entities.Task task)
        {
            try
            {
                if (task == null)
                    return BadRequest();

                var createdTask = await _taskBLL.AddTask(task);

                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new task record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<DataAccessLayer.Entities.Task>> UpdateTask(int id, DataAccessLayer.Entities.Task task)
        {
            try
            {
                if (id != task.Id)
                    return BadRequest("Project Id mismatch");

                var taskToUpdate = await _taskBLL.GetTask(id);

                if (taskToUpdate == null)
                {
                    return NotFound($"Task with Id = {id} not found");
                }

              

                return await _taskBLL.UpdateTask(task);


            }
            catch (Exception)
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
