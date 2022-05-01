using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web_API.Models;
using System;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly ITasksepository taskRepository;

        public TasksController(ITasksepository taskRepository)
        {
            this.taskRepository = taskRepository;

        }

        [HttpGet]
        public async Task<ActionResult> GetTasks()
        {

            try
            {
                return  Ok(await taskRepository.GetTasks());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");

            }
            
           
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            try
            {
                var result = await taskRepository.GetTask(id);

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
        public async Task<ActionResult<Models.Task>> CreateTaskt(Models.Task task)
        {
            try
            {
                if (task == null)
                    return BadRequest();

                var createdTask = await taskRepository.AddTask(task);

                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new task record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Models.Task>> UpdateTask(int id, Models.Task task)
        {
            try
            {
                if (id != task.Id)
                    return BadRequest("Project Id mismatch");

                var taskToUpdate = await taskRepository.GetTask(id);

                if (taskToUpdate == null)
                {
                    return NotFound($"Task with Id = {id} not found");
                }

              

                return await taskRepository.UpdateTask(task);


            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating task record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Models.Task>> DeleteTask(int id)
        {
            try
            {


                var taskToDelete = await taskRepository.GetTask(id);

                if (taskToDelete == null)
                {
                    return NotFound($"Task with Id = {id} not found");
                }



                return Ok(await taskRepository.DeleteTask(id));


            }
            catch (Exception )
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting task record");
            }
        }


    }
}
