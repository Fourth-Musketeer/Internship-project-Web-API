
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Web_API.Controllers;
using WebAPIShared.Enums;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace WebApiTests
{
    public class ProjectsControllerTest 
    {
        private readonly ProjectsController _projectsControllerTest; 
        private readonly Mock<IProjectServices> _projectServicesMock = new Mock<IProjectServices>(); 

        private readonly IEnumerable<Project> _projects; 
        public ProjectsControllerTest()
        {
            _projectsControllerTest = new ProjectsController(_projectServicesMock.Object);

            _projects = new List<Project>()
            {
                new Project(){Id=1, CurrentStatus=CurrentProjectStatus.Completed ,Name="project1",StartDate=DateTime.Now,CompletionDate=DateTime.Now,Priority=1 },
                 new Project(){Id=2, CurrentStatus=CurrentProjectStatus.Active, Name="project2",StartDate=DateTime.Now,CompletionDate=null,Priority=9 },
                  new Project(){Id=3, CurrentStatus=CurrentProjectStatus.NotStarted, Name="project3",StartDate=null,CompletionDate=null,Priority=4 },
            };
        }




        [Fact]
        public async Task GetProject_shouldReturnProject_IfProjectExist()
        {
            //Arange 

            var project = _projects.ElementAt(0);//Fake project we got from our fake project list 

            _projectServicesMock.Setup(x => x.GetProject(project.Id)).ReturnsAsync(_projects.ElementAt(0));
                                                                                                   
            //act
            var projectDTO = await _projectsControllerTest.GetProject(project.Id);
          
            //Assert

            Assert.Equal(project.Id, projectDTO.Value.Id);                                                  
            Assert.Equal(project.Name, projectDTO.Value.Name);


        }

        [Fact]

        public async Task GetProject_ShouldReturnNotFound_When_Project_DoesNotExist()
        {

            //Arange                              
            _projectServicesMock.Setup(x => x.GetProject(It.IsAny<int>())).ReturnsAsync(() => null); 
                                                                                            



            //Act
            var projectDTO = await _projectsControllerTest.GetProject(GetHashCode());
            var StatusCodeResult = (StatusCodeResult)projectDTO.Result;


            //Assert

            Assert.Equal((int)HttpStatusCode.NotFound, StatusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateProjectShouldReturnProjectIfEverythingWasInsertedCorrectly()
        {
            //Arange
            var projectModel = new ProjectModel
            {
                Name = "Project2",
                Priority = 2,
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now
            };

            Project project = new Project
            {
                Id = 4,
                Name = "Project2",
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                Priority = 2,
                CurrentStatus = CurrentProjectStatus.Completed

            };

            _projectServicesMock.Setup(x =>x.GetProjectByName(projectModel.Name)).ReturnsAsync(()=>null);
            _projectServicesMock.Setup(x => x.AddProject(projectModel)).ReturnsAsync(project);


            //Act
            var result = await _projectsControllerTest.CreateProject(projectModel);




            //Assert

            Assert.Equal(project.Name, result.Value.Name);
            Assert.Equal(project.Priority, result.Value.Priority);
            Assert.Equal(project.CompletionDate, result.Value.CompletionDate);


        }

        [Fact]
        public async Task UpdateProjectShouldUpdateProjectIfEverythingIsOk()
        {
            var projectModel = new ProjectModel
            {
                Name = "Project243",
                Priority = 4,
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now
            };
            var project = new Project
            {   Id=3,
                Name = "Project243",
                Priority = 4,
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                CurrentStatus =CurrentProjectStatus.Completed
                
            };
            _projectServicesMock.Setup(x => x.GetProject(_projects.ElementAt(2).Id)).ReturnsAsync(_projects.ElementAt(2));
            _projectServicesMock.Setup(x => x.UpdateProject(_projects.ElementAt(2).Id,projectModel)).ReturnsAsync(project);

            var result = await _projectsControllerTest.UpdateProject(_projects.ElementAt(2).Id, projectModel);

            Assert.Equal(project.Name,result.Value.Name);
            Assert.Equal(project.Priority, result.Value.Priority);
            Assert.Equal(project.StartDate, result.Value.StartDate);

        }


        //[Fact]
        //public async Task DeleteProjectShouldDeleteProjectIfProjectExists()
        //{
        //    var projectId = _projects.ElementAt(1).Id;

        //    _projectBLLMock.Setup(x => x.GetProject(projectId)).ReturnsAsync(_projects.ElementAt(1));
        //    _projectBLLMock.Setup(x => x.DeleteProject(projectId)).ReturnsAsync(_projects.ElementAt(1));


        //    var result = await _projectsControllerTest.DeleteProject(projectId);
        //    var expected = (StatusCodeResult)result.Result;


        //    Assert.Equal((int)HttpStatusCode.OK, expected.StatusCode);
        //}



    }
}
