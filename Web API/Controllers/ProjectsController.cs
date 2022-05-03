using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using BusinessLogicLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;

namespace Web_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectBLL _projectBLL;

        public ProjectsController(IProjectBLL projectBLL)
        {
            _projectBLL = projectBLL;
        }

        [HttpGet]
        public async Task<ActionResult> GetProjets()
        {
            try
            {
                return Ok(await _projectBLL.GetProjects());
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
                var result = await _projectBLL.GetProject(id);

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

                var p = _projectBLL.GetProjectByName(project.Name).Result;

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

                else if (project.StartDate==null && project.CompletionDate.HasValue)
                {
                    ModelState.AddModelError("CompletionDate", "Start date is null and completion date has value");
                    return BadRequest(ModelState);
                }



                var createdProject = await _projectBLL.AddProject(project);

                return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
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
                


                var projectToUpdate = await _projectBLL.GetProject(id);
               

                if (projectToUpdate == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }

                var p = _projectBLL.GetProjectByNameAndId(project.Name,id).Result;

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


                return await _projectBLL.UpdateProject(id,project);


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


                var projectToDelete = await _projectBLL.GetProject(id);

                if (projectToDelete == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }



                return Ok(await _projectBLL.DeleteProject(id));


            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting project record");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Project>>> Search(string name, int priority, CurrentProjectStatus? currentProjectStatus)
        {
            try
            {
                var result = await _projectBLL.Search(name, priority, currentProjectStatus);

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

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Project>> Patch(int id, [FromBody] JsonPatchDocument<Project> patchEntity)
        {

            try
            {
                return Ok(await _projectBLL.UpdateProjectPatch(id, patchEntity));
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error patching data");
            }
           
           
        }




    }
 
}
