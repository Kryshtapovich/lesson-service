using Domain.Models.Student;
using Domain.Models.Survey;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>().HasMany(g => g.Students).WithOne();

            builder.Entity<Survey>().HasMany(s => s.Questions).WithOne();
            builder.Entity<Question>().HasMany(q => q.Options).WithOne();
        }
    }
}