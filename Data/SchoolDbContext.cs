using DuAnThucTap.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; // Đảm bảo đã có using này
namespace DuAnThucTap.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        // DbSets for each entity
        public DbSet<SchoolInformation>? SchoolInformation { get; set; }
        public DbSet<SchoolYear>? SchoolYears { get; set; }
        public DbSet<Semester>? Semesters { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<DepartmentLeader>? DepartmentLeaders { get; set; }
        public DbSet<Campus>? Campuses { get; set; }
        public DbSet<SubjectType>? SubjectTypes { get; set; }
        public DbSet<Subject>? Subjects { get; set; }
        public DbSet<GradeLevel>? GradeLevels { get; set; }
        public DbSet<ClassType>? ClassTypes { get; set; }
        public DbSet<Class>? Classes { get; set; }
        public DbSet<TeachingAssignment>? TeachingAssignments { get; set; }
        public DbSet<TopicList>? TopicLists { get; set; }
        public DbSet<BlockLeader>? BlockLeaders { get; set; }
        public DbSet<GradeType>? GradeTypes { get; set; }
        public DbSet<Grade>? Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -----------------------------------------------------------
            // SchoolInformation (Không có FK, PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------

            // -----------------------------------------------------------
            // SchoolYear (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<SchoolYear>()
                .HasMany(sy => sy.Semesters)
                .WithOne(s => s.SchoolYear)
                .HasForeignKey(s => s.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa SchoolYear sẽ xóa các Semesters liên quan

            modelBuilder.Entity<SchoolYear>()
                .HasMany(sy => sy.Classes)
                .WithOne(c => c.SchoolYear)
                .HasForeignKey(c => c.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa SchoolYear sẽ xóa các Classes liên quan

            modelBuilder.Entity<SchoolYear>()
                .HasMany(sy => sy.Subjects)
                .WithOne(s => s.SchoolYear)
                .HasForeignKey(s => s.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.SetNull); // Xóa SchoolYear sẽ đặt FK_SchoolYearID trong Subjects thành NULL (nếu nullable)

            modelBuilder.Entity<SchoolYear>()
                .HasMany(sy => sy.GradeLevels)
                .WithOne(gl => gl.SchoolYear)
                .HasForeignKey(gl => gl.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.SetNull); // Xóa SchoolYear sẽ đặt FK_SchoolYearID trong GradeLevels thành NULL (nếu nullable)

            modelBuilder.Entity<SchoolYear>()
                .HasMany(sy => sy.BlockLeaders)
                .WithOne(bl => bl.SchoolYear)
                .HasForeignKey(bl => bl.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa SchoolYear sẽ xóa các BlockLeaders liên quan

            modelBuilder.Entity<SchoolYear>()
                .HasMany(sy => sy.Grades)
                .WithOne(g => g.SchoolYear)
                .HasForeignKey(g => g.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa SchoolYear sẽ xóa các Grades liên quan

            // -----------------------------------------------------------
            // Semester (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<Semester>()
                .HasOne(s => s.SchoolYear)
                .WithMany(sy => sy.Semesters)
                .HasForeignKey(s => s.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Restrict); // Giả định bạn không muốn xóa SchoolYear nếu còn Semester liên quan, hoặc để Cascade nếu muốn xóa theo.

            modelBuilder.Entity<Semester>()
                .HasMany(s => s.TopicLists)
                .WithOne(tl => tl.Semester)
                .HasForeignKey(tl => tl.FK_SemesterID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Semester sẽ xóa các TopicLists liên quan

            modelBuilder.Entity<Semester>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Semester)
                .HasForeignKey(g => g.FK_SemesterID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Semester sẽ xóa các Grades liên quan

            // -----------------------------------------------------------
            // Department (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<Department>()
                .HasMany(d => d.DepartmentLeaders)
                .WithOne(dl => dl.Department)
                .HasForeignKey(dl => dl.FK_DepartmentID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Department sẽ xóa các DepartmentLeaders liên quan

            // -----------------------------------------------------------
            // DepartmentLeader (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<DepartmentLeader>()
                .HasOne(dl => dl.Department)
                .WithMany(d => d.DepartmentLeaders)
                .HasForeignKey(dl => dl.FK_DepartmentID)
                .OnDelete(DeleteBehavior.Restrict); // Giả định bạn không muốn xóa Department nếu còn DepartmentLeader liên quan

            // -----------------------------------------------------------
            // Campus (Không có FK, PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------

            // -----------------------------------------------------------
            // SubjectType (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<SubjectType>()
                .HasMany(st => st.Subjects)
                .WithOne(s => s.SubjectType)
                .HasForeignKey(s => s.FK_SubjectTypeID)
                .OnDelete(DeleteBehavior.Restrict); // Giả định bạn không muốn xóa SubjectType nếu còn Subjects liên quan

            // -----------------------------------------------------------
            // Subject (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.SubjectType)
                .WithMany(st => st.Subjects)
                .HasForeignKey(s => s.FK_SubjectTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.SchoolYear)
                .WithMany(sy => sy.Subjects)
                .HasForeignKey(s => s.FK_SchoolYearID)
                .IsRequired(false) // Đảm bảo rằng FK_SchoolYearID là nullable trong DB
                .OnDelete(DeleteBehavior.SetNull); // Nếu SchoolYear bị xóa, set FK_SchoolYearID thành NULL

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.TeachingAssignments)
                .WithOne(ta => ta.Subject)
                .HasForeignKey(ta => ta.FK_SubjectID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Subject sẽ xóa các TeachingAssignments liên quan

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Subject)
                .HasForeignKey(g => g.FK_SubjectID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Subject sẽ xóa các Grades liên quan

            // -----------------------------------------------------------
            // GradeLevel (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<GradeLevel>()
                .HasOne(gl => gl.SchoolYear)
                .WithMany(sy => sy.GradeLevels)
                .HasForeignKey(gl => gl.FK_SchoolYearID)
                .IsRequired(false) // Đảm bảo rằng FK_SchoolYearID là nullable trong DB
                .OnDelete(DeleteBehavior.SetNull); // Nếu SchoolYear bị xóa, set FK_SchoolYearID thành NULL

            modelBuilder.Entity<GradeLevel>()
                .HasMany(gl => gl.Classes)
                .WithOne(c => c.GradeLevel)
                .HasForeignKey(c => c.FK_GradeLevelID)
                .OnDelete(DeleteBehavior.Restrict); // Giả định không xóa GradeLevel nếu còn Classes liên quan

            modelBuilder.Entity<GradeLevel>()
                .HasMany(gl => gl.BlockLeaders)
                .WithOne(bl => bl.GradeLevel)
                .HasForeignKey(bl => bl.FK_GradeLevelID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa GradeLevel sẽ xóa các BlockLeaders liên quan

            // -----------------------------------------------------------
            // ClassType (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<ClassType>()
                .HasMany(ct => ct.Classes)
                .WithOne(c => c.ClassType)
                .HasForeignKey(c => c.FK_ClassTypeID)
                .OnDelete(DeleteBehavior.Restrict); // Giả định không xóa ClassType nếu còn Classes liên quan

            modelBuilder.Entity<ClassType>()
                .HasMany(ct => ct.TeachingAssignments) // Note: ClassType also has FK in TeachingAssignments
                .WithOne(ta => ta.ClassType)
                .HasForeignKey(ta => ta.FK_ClassTypeID)
                .IsRequired(false) // Dựa trên sơ đồ, FK này có thể là nullable
                .OnDelete(DeleteBehavior.SetNull); // Nếu ClassType bị xóa, set FK thành NULL trong TeachingAssignments

            modelBuilder.Entity<ClassType>()
                .HasMany(ct => ct.Grades)
                .WithOne(g => g.ClassType)
                .HasForeignKey(g => g.FK_ClassTypeID)
                .IsRequired(false) // Dựa trên sơ đồ, FK này có thể là nullable
                .OnDelete(DeleteBehavior.SetNull); // Nếu ClassType bị xóa, set FK thành NULL trong Grades

            // -----------------------------------------------------------
            // Class (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<Class>()
                .HasOne(c => c.SchoolYear)
                .WithMany(sy => sy.Classes)
                .HasForeignKey(c => c.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa SchoolYear nếu còn Classes

            modelBuilder.Entity<Class>()
                .HasOne(c => c.GradeLevel)
                .WithMany(gl => gl.Classes)
                .HasForeignKey(c => c.FK_GradeLevelID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa GradeLevel nếu còn Classes

            modelBuilder.Entity<Class>()
                .HasOne(c => c.ClassType)
                .WithMany(ct => ct.Classes)
                .HasForeignKey(c => c.FK_ClassTypeID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa ClassType nếu còn Classes

            // Nếu Teacher Model tồn tại
            // modelBuilder.Entity<Class>()
            //     .HasOne<Teacher>() // Tên của Teacher Model
            //     .WithMany() // Nếu Teacher có nhiều Class, hoặc WithOne nếu 1-1
            //     .HasForeignKey(c => c.FK_TeacherID)
            //     .IsRequired(false) // Vì FK_TeacherID là nullable
            //     .OnDelete(DeleteBehavior.SetNull); // Khi Teacher bị xóa, set FK thành NULL

            modelBuilder.Entity<Class>()
                .HasMany(c => c.TeachingAssignments)
                .WithOne(ta => ta.Class)
                .HasForeignKey(ta => ta.FK_ClassID)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Class sẽ xóa các TeachingAssignments liên quan

            // -----------------------------------------------------------
            // TeachingAssignment (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(ta => ta.Class)
                .WithMany(c => c.TeachingAssignments)
                .HasForeignKey(ta => ta.FK_ClassID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Class nếu còn TeachingAssignment

            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(ta => ta.Subject)
                .WithMany(s => s.TeachingAssignments)
                .HasForeignKey(ta => ta.FK_SubjectID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Subject nếu còn TeachingAssignment

            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(ta => ta.ClassType)
                .WithMany(ct => ct.TeachingAssignments)
                .HasForeignKey(ta => ta.FK_ClassTypeID)
                .IsRequired(false) // Dựa trên sơ đồ, FK này có thể là nullable
                .OnDelete(DeleteBehavior.SetNull); // Nếu ClassType bị xóa, set FK thành NULL

            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(ta => ta.SchoolYear)
                .WithMany() // TeachingAssignment có FK_SchoolYearID nhưng SchoolYear không có navigation property ngược lại chỉ cho TeachingAssignment
                .HasForeignKey(ta => ta.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa SchoolYear nếu còn TeachingAssignment

            // -----------------------------------------------------------
            // TopicList (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<TopicList>()
                .HasOne(tl => tl.Semester)
                .WithMany(s => s.TopicLists)
                .HasForeignKey(tl => tl.FK_SemesterID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Semester nếu còn TopicList

            // -----------------------------------------------------------
            // BlockLeader (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<BlockLeader>()
                .HasOne(bl => bl.GradeLevel)
                .WithMany(gl => gl.BlockLeaders)
                .HasForeignKey(bl => bl.FK_GradeLevelID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa GradeLevel nếu còn BlockLeader

            modelBuilder.Entity<BlockLeader>()
                .HasOne(bl => bl.SchoolYear)
                .WithMany(sy => sy.BlockLeaders)
                .HasForeignKey(bl => bl.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa SchoolYear nếu còn BlockLeader

            // -----------------------------------------------------------
            // GradeType (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            modelBuilder.Entity<GradeType>()
                .HasMany(gt => gt.Grades)
                .WithOne(g => g.GradeType)
                .HasForeignKey(g => g.FK_GradeTypeID)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa GradeType nếu còn Grades

            // -----------------------------------------------------------
            // Grade (PK được định nghĩa bằng [Key])
            // -----------------------------------------------------------
            // Giả định StudentID là một cột thông thường hoặc được xử lý bên ngoài EF Core
            // Nếu Student là một entity trong DBContext này, bạn sẽ cần cấu hình FK.

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.FK_SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Semester)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.FK_SemesterID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.GradeType)
                .WithMany(gt => gt.Grades)
                .HasForeignKey(g => g.FK_GradeTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.SchoolYear)
                .WithMany(sy => sy.Grades)
                .HasForeignKey(g => g.FK_SchoolYearID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.ClassType)
                .WithMany(ct => ct.Grades)
                .HasForeignKey(g => g.FK_ClassTypeID)
                .IsRequired(false) // Dựa trên sơ đồ, FK này có thể là nullable
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}