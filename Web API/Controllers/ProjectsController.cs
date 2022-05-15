using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using DataAccessLayer.Entities;
using WebAPIShared.Enums;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;

namespace Web_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectServices _projectServices;

        public ProjectsController(IProjectServices projectServices)
        {
            _projectServices = projectServices;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjets()
        {
            try
            {
                return Ok(await _projectServices.GetProjects());
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            try
            {
                var result = await _projectServices.GetProject(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(ProjectModel project)
        {
            try
            {
                if (project == null)
                    return BadRequest();

                var p = _projectServices.GetProjectByName(project.Name).Result;

                if (p != null)
                {
                    ModelState.AddModelError("Name", "Project with that name has already been created");
                    return BadRequest(ModelState);
                }

                else if (project.StartDate > project.CompletionDate)
                {
                    ModelState.AddModelError("CompletionDate", "Completion date cannon be earlier that start date");
                    return BadRequest(ModelState);
                }

                else if (project.StartDate == null && project.CompletionDate.HasValue)
                {
                    ModelState.AddModelError("CompletionDate", "Start date is null and completion date has value");
                    return BadRequest(ModelState);
                }
                else if (!int.TryParse(project.Priority.ToString(), out _))
                {
                    ModelState.AddModelError("Priority", "Only numbers are alowed in priority (1-10)");
                    return BadRequest(ModelState);
                }



                var createdProject = await _projectServices.AddProject(project);

                // return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);

                return createdProject;
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new project record");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<Project>> UpdateProject(int id, ProjectModel project)
        {
            try
            {
                


                var projectToUpdate = await _projectServices.GetProject(id);
               

                if (projectToUpdate == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }

                var p = _projectServices.GetProjectByNameAndId(project.Name,id).Result;

                if (p != null)
                {
                    ModelState.AddModelError("Name", "Project with that name has already been created");
                    return BadRequest(ModelState);
                }


                if (project.StartDate > project.CompletionDate)
                {
                    ModelState.AddModelError("CompletionDate", "Completion date cannon be earlier that start date");
                    return BadRequest(ModelState);
                }
                else if (project.StartDate == null && project.CompletionDate.HasValue)
                {
                    ModelState.AddModelError("CompletionDate", "Start date is null and completion date has value");
                    return BadRequest(ModelState);
                }


                return await _projectServices.UpdateProject(id,project);


            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating project record");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            try
            {


                var projectToDelete = await _projectServices.GetProject(id);

                if (projectToDelete == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }



               // return Ok(await _projectBLL.DeleteProject(id));
                return StatusCode(StatusCodes.Status200OK, await _projectServices.DeleteProject(id));


            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting project record");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Project>>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus, string sort)
        {
            try
            {
                var result = await _projectServices.Search(name, priority, currentProjectStatus, sort);

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception )
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}/Tasks")]
        public async Task<ActionResult<IEnumerable<DataAccessLayer.Entities.Task>>> FindAllTasks(int id)
        {
            try
            {
                var result = await _projectServices.FindAllTasks(id);
               

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving tasks from the database");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Project>> Patch(int id, [FromBody] JsonPatchDocument<Project> patchEntity)
        {

            try
            {
                return Ok(await _projectServices.UpdateProjectPatch(id, patchEntity));
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error patching data");
            }
 
           
        }



    }
 
}
