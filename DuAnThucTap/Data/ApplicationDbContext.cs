using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 🔹 DbSet cho tất cả entity
        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<Classtype> Classtypes { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Grade> Grades { get; set; } = null!;
        public DbSet<Gradelevel> Gradelevels { get; set; } = null!;
        public DbSet<Schoolinformation> Schoolinformations { get; set; } = null!;
        public DbSet<Schoolyear> Schoolyears { get; set; } = null!;
        public DbSet<Semester> Semesters { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Subjecttype> Subjecttypes { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Teachingassignment> Teachingassignments { get; set; } = null!;
        public DbSet<Topiclist> Topiclists { get; set; } = null!;
        public DbSet<Departmentleader> Departmentleaders { get; set; } = null!;
        public DbSet<ClassSubject> ClassSubjects { get; set; } = null!;
        public DbSet<Blockleader> Blockleaders { get; set; } = null!;
        public DbSet<Campus> Campuses { get; set; } = null!;
        public DbSet<Gradetype> Gradetypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔹 Schoolinformation
            modelBuilder.Entity<Schoolinformation>()
                .HasKey(s => s.Schoolinfoid);

            // 🔹 Teachingassignment
            modelBuilder.Entity<Teachingassignment>()
                .HasKey(t => t.Assignmentid);

            modelBuilder.Entity<Teachingassignment>()
                .HasOne(t => t.Topic)
                .WithMany(tl => tl.Teachingassignments)
                .HasForeignKey(t => t.Topicid);

            // 🔹 ClassSubject (nhiều-nhiều)
            modelBuilder.Entity<ClassSubject>()
                .HasKey(cs => new { cs.Classid, cs.Subjectid });

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(cs => cs.Classid);

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.ClassSubjects)
                .HasForeignKey(cs => cs.Subjectid);

            base.OnModelCreating(modelBuilder); // ✅ Gọi đúng 1 lần, cuối cùng
        }
    }
}
