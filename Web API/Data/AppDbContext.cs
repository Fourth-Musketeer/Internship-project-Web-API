using Microsoft.EntityFrameworkCore;
using Web_API.Models;

namespace Web_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)// this instance carries configuration informations
            : base(options)
        {
             
        }



        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }

    }

   
}
