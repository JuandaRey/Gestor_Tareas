using TaskManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // RELACIÓN: Student 1 - N Tasks
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Student)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN: Course 1 - N Tasks
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
