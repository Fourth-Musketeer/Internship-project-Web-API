using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace Web_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProjets()
        {
            try
            {
                return Ok(await projectRepository.GetProjects());
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
                var result = await projectRepository.GetProject(id);

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
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            try
            {
                if (project == null)
                    return BadRequest();

                var p = projectRepository.GetProjectByName(project.Name).Result;

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

                var createdProject = await projectRepository.AddProject(project);

                return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new project record");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<Project>> UpdateProject(int id, Project project)
        {
            try
            {
                if (id != project.Id)
                    return BadRequest("Project Id mismatch");


                var projectToUpdate = await projectRepository.GetProject(id);
               

                if (projectToUpdate == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }

                var p = projectRepository.GetProjectByName(project.Name).Result;

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

                return await projectRepository.UpdateProject(project);


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


                var projectToDelete = await projectRepository.GetProject(id);

                if (projectToDelete == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }



                return Ok(await projectRepository.DeleteProject(id));


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
                var result = await projectRepository.Search(name, priority, currentProjectStatus);

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

            return Ok(await projectRepository.UpdateProjectPatch(id, patchEntity));
           
        }




    }
    //public bool  ValidateDate(DateTime begin, DateTime? end)
    //{
    //    if (begin > end)
    //        return true;

    //    return false;
    //}
}
