using BuildYourself.Domain.Enities;
using Microsoft.EntityFrameworkCore;

namespace BuildYourself.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        } 
        public DbSet<FileItem> Files { get; set; }
        public DbSet<GymTask> GymTasks { get; set; }
        public DbSet<GymCategory> GymCategories { get; set; }
        public DbSet<FileCategory> FileCategories { get; set; }

    }
}
