using Autofac.Extras.Moq;
using BusinessLogicLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using WebAPIShared.Enums;

namespace WebApiTests
{
    public class ProjectServiceTest
    {
        private readonly IEnumerable<Project> _projects;
        private readonly ProjectServices _SistemUnderTest;
        private readonly Mock<IProjectRepository> _projectRepoMock = new Mock<IProjectRepository>();

        public ProjectServiceTest()
        {
            _projects = new List<Project>()
            {
                new Project(){Id=1, CurrentStatus=CurrentProjectStatus.Completed ,Name="project1",StartDate=DateTime.Now,CompletionDate=DateTime.Now,Priority=1 },
                 new Project(){Id=2, CurrentStatus=CurrentProjectStatus.Active, Name="project2",StartDate=DateTime.Now,CompletionDate=null,Priority=9 },
                  new Project(){Id=3, CurrentStatus=CurrentProjectStatus.NotStarted, Name="project3",StartDate=null,CompletionDate=null,Priority=4 },
            };

            _SistemUnderTest = new ProjectServices(_projectRepoMock.Object);

        }



        [Fact]
        public async Task GetProjects_shouldReturnAllProjects_WhenTheyExist()
        {
            //Arange 
            
            _projectRepoMock.Setup(x => x.GetProjects()).ReturnsAsync(_projects);  

            //Act
            var project = await _SistemUnderTest.GetProjects();

            //Asert
            Assert.Equal(3, project.Count());

        }

        [Fact]
        public async Task GetProjects_ShouldReturnNull_WhenTheyDoNotExist()
        {
            //Arange 

           _projectRepoMock.Setup(x => x.GetProjects()).ReturnsAsync(()=>null);  
            //Act
            var project = await _SistemUnderTest.GetProjects();

            //Asert
            Assert.Null(project);

        }

      


    }
  





}

