using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer
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
