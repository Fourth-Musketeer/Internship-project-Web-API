# ASP.NET CORE 5.0 WEB API Internship project

Explanation of project and how to use it

Dependencies used in project 'DataAccessLayer':
* Microsoft.EntityFrameworkCore.Tools v5.0.16
* Microsoft.EntityFrameworkCore.SqlServer v5.0.16
* Microsoft.EntityFrameworkCore v5.0.16
* Microsoft.AspNetCore.Mvc.NewtonsoftJson v5.0.16

Dependencies used in project 'WebAPI':
* Swashbuckle.AspNetCore v5.6.3
* Microsoft.Entity.FrameworkCore.Design v5.0.16
* Microsoft.AspNetCore.Mvc.NewtonsoftJson v5.0.16

MS SQL Server was used for database
Unit Tests were created using xUnit and Moq


After downloading solution go to WebAPI project and in appsettings.json provide name of the local server. After name is provided, type command 'Update-Database' in package manager console. Database should be created and project is ready to use.
In database we have 2 enities. Projects and tasks. 1 project can have multiple tasks, 1 task must belong to only 1 project. User can add,create,view and delete projects and tasks. Swagger was used for documenting API. Special cases are covered, user cannot break application. 

