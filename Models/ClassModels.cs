using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace DuAnThucTap.Models
{
    // --- SchoolInformation.cs ---
    public class SchoolInformation
    {
        [Key]
        public int SchoolInfoID { get; set; }

        [Required]
        [StringLength(255)]
        public string? SchoolName { get; set; }

        [StringLength(50)]
        public string? StandardCode { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? Province { get; set; }

        [StringLength(100)]
        public string? Ward { get; set; }

        [StringLength(100)]
        public string? District { get; set; }

        [StringLength(100)]
        public string? SchoolType { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(20)]
        public string? FaxNumber { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        public DateTime? EstablishmentDate { get; set; }

        [StringLength(255)]
        public string? TrainingModel { get; set; }

        [StringLength(255)]
        public string? WebsiteURL { get; set; }

        [StringLength(255)]
        public string? PrincipalName { get; set; }

        [StringLength(20)]
        public string? PrincipalPhone { get; set; }

        [StringLength(255)]
        public string? LogoURL { get; set; }
    }

    // --- SchoolYear.cs ---
    public class SchoolYear
    {
        [Key]
        public int SchoolYearID { get; set; }

        [Required]
        [StringLength(50)]
        public string? SchoolYearName { get; set; }

        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        // Navigation properties
        [JsonIgnore]
        public ICollection<Semester>? Semesters { get; set; }
        [JsonIgnore]
        public ICollection<Class>? Classes { get; set; }
        [JsonIgnore] public ICollection<Subject>? Subjects { get; set; } // Assuming Subjects can be linked to SchoolYear
        [JsonIgnore] public ICollection<GradeLevel>? GradeLevels { get; set; } // Assuming GradeLevels can be linked to SchoolYear
        [JsonIgnore] public ICollection<BlockLeader>? BlockLeaders { get; set; }
        [JsonIgnore] public ICollection<Grade>? Grades { get; set; } // Grades are tied to SchoolYear
       
    }

    // --- Semester.cs ---
    public class Semester
    {
        [Key]
        public int SemesterID { get; set; }

        [Required]
        [StringLength(50)]
        public string? SemesterName { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        [ForeignKey("SchoolYear")]
        public int FK_SchoolYearID { get; set; }
        [JsonIgnore] public SchoolYear? SchoolYear { get; set; }

        // Navigation properties
        [JsonIgnore] public ICollection<TopicList>? TopicLists { get; set; }
        [JsonIgnore] public ICollection<Grade>? Grades { get; set; } // Grades are tied to Semester
    }

    // --- Department.cs ---
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(255)]
        public string? DepartmentName { get; set; }

        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        // Navigation properties
        [JsonIgnore] public ICollection<DepartmentLeader>? DepartmentLeaders { get; set; }
    }

    // --- DepartmentLeader.cs ---
    public class DepartmentLeader
    {
        [Key]
        public int DepartmentLeaderID { get; set; }

        [ForeignKey("Department")]
        public int FK_DepartmentID { get; set; }
        [JsonIgnore] public Department? Department { get; set; }

        [StringLength(255)]
        public string? DepartmentLeaderName { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }

    // --- Campus.cs ---
    public class Campus
    {
        [Key]
        public int CampusID { get; set; }

        [Required]
        [StringLength(255)]
        public string? CampusName { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(255)]
        public string? ImageURL { get; set; }

        [StringLength(255)]
        public string? ContactPersonName { get; set; }

        [StringLength(20)]
        public string? ContactPersonMobile { get; set; }

        [StringLength(255)]
        public string? ContactPersonEmail { get; set; }
    }

    // --- SubjectType.cs ---
    public class SubjectType
    {
        [Key]
        public int SubjectTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string? SubjectTypeName { get; set; }

        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        // Navigation properties
        [JsonIgnore] public ICollection<Subject>? Subjects { get; set; }
    }

    // --- Subject.cs ---
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        [Required]
        [StringLength(255)]
        public string? SubjectName { get; set; }

        [ForeignKey("SubjectType")]
        public int FK_SubjectTypeID { get; set; }
        [JsonIgnore] public SubjectType? SubjectType { get; set; }

        [StringLength(50)]
        public string? DefaultPeriodsSem1 { get; set; }

        [StringLength(50)]
        public string? DefaultPeriodsSem2 { get; set; }

        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        [ForeignKey("SchoolYear")]
        public int? FK_SchoolYearID { get; set; } // Nullable if subject can exist without a specific school year, but your diagram shows FK.
        [JsonIgnore] public SchoolYear? SchoolYear { get; set; }

        // Navigation properties
        [JsonIgnore] public ICollection<TeachingAssignment>? TeachingAssignments { get; set; }
        [JsonIgnore] public ICollection<Grade>? Grades { get; set; }
    }

    // --- GradeLevel.cs ---
    public class GradeLevel
    {
        [Key]
        public int GradeLevelID { get; set; }

        [Required]
        [StringLength(50)]
        public string? GradeLevelName { get; set; }

        public int? GradeCodeLevel { get; set; } // Assuming it's an integer code

        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        [ForeignKey("SchoolYear")]
        public int? FK_SchoolYearID { get; set; } // Nullable if grade level can exist without a specific school year, but your diagram shows FK.
        [JsonIgnore] public SchoolYear? SchoolYear { get; set; }

        [JsonIgnore] public ICollection<BlockLeader>? BlockLeaders { get; set; }

        // Navigation properties
        [JsonIgnore] public ICollection<Class>? Classes { get; set; }
    }

    // --- ClassType.cs ---
    public class ClassType
    {
        [Key]
        public int ClassTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string? ClassTypeName { get; set; }

        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        // Navigation properties
        [JsonIgnore] public ICollection<Class>? Classes { get; set; }
        [JsonIgnore] public ICollection<Grade>? Grades { get; set; }
        [JsonIgnore] public ICollection<TeachingAssignment>? TeachingAssignments { get; set; }
    }

    // --- Class.cs ---
    public class Class
    {
        [Key]
        public int ClassID { get; set; }

        [Required]
        [StringLength(255)]
        public string? ClassName { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public int MaxStudents { get; set; }

        [ForeignKey("SchoolYear")]
        public int FK_SchoolYearID { get; set; }
        [JsonIgnore] public SchoolYear? SchoolYear { get; set; }

        [ForeignKey("GradeLevel")]
        public int FK_GradeLevelID { get; set; }
        [JsonIgnore] public GradeLevel? GradeLevel { get; set; }

        [ForeignKey("ClassType")]
        public int FK_ClassTypeID { get; set; }
        [JsonIgnore] public ClassType? ClassType { get; set; }

        [ForeignKey("Teacher")] // Assuming there's a Teacher table not shown, or this is just an ID from another source
        public int? FK_TeacherID { get; set; } // Teacher can be null

        // Navigation properties
        [JsonIgnore] public ICollection<TeachingAssignment>? TeachingAssignments { get; set; }
    }

    // --- TeachingAssignment.cs ---
    public class TeachingAssignment
    {
        [Key]
        public int AssignmentID { get; set; }

        [ForeignKey("Class")]
        public int FK_ClassID { get; set; }
        [JsonIgnore] public Class? Class { get; set; }

        [ForeignKey("Subject")]
        public int FK_SubjectID { get; set; }
        [JsonIgnore] public Subject? Subject { get; set; }

        [ForeignKey("ClassType")]
        public int? FK_ClassTypeID { get; set; } // Nullable based on diagram
        [JsonIgnore] public ClassType? ClassType { get; set; }

        [ForeignKey("SchoolYear")]
        public int FK_SchoolYearID { get; set; }
        [JsonIgnore] public SchoolYear? SchoolYear { get; set; }

        public DateTime? TeachingStartDate { get; set; }
        public DateTime? TeachingEndDate { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }
    }

    // --- TopicList.cs ---
    public class TopicList
    {
        [Key]
        public int TopicID { get; set; }

        [Required]
        [StringLength(255)]
        public string? TopicName { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? TeachingStartDate { get; set; }
        public DateTime? TeachingEndDate { get; set; }

        [ForeignKey("Semester")]
        public int FK_SemesterID { get; set; }
        [JsonIgnore] public Semester? Semester { get; set; }
    }

    // --- BlockLeader.cs ---
    public class BlockLeader
    {
        [Key]
        public int BlockLeaderID { get; set; }

        [ForeignKey("GradeLevel")]
        public int FK_GradeLevelID { get; set; }
        [JsonIgnore] public GradeLevel? GradeLevel { get; set; }

        [ForeignKey("SchoolYear")]
        public int FK_SchoolYearID { get; set; }
        [JsonIgnore] public SchoolYear? SchoolYear { get; set; }

        [StringLength(255)]
        public string? BlockLeaderName { get; set; }
        // Xóa dòng này
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? UpdateAt { get; set; }
    }

    // --- GradeType.cs ---
    public class GradeType
    {
        [Key]
        public int GradeTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string? GradeTypeName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public double WeightingFactor { get; set; }

        public double MinInstancesSemester1 { get; set; }
        public double MinInstancesSemester2 { get; set; }

        // Navigation properties
        [JsonIgnore] public ICollection<Grade>? Grades { get; set; }
    }

    // --- Grade.cs ---
    public class Grade
    {
        [Key]
        public int GradeID { get; set; }

        // Assuming StudentID comes from a separate Student table, not shown here.
        // If Student is part of the same DBContext, you'd add a Student model and FK.
        public int StudentID { get; set; }

        [ForeignKey("Subject")]
        public int FK_SubjectID { get; set; }
        [JsonIgnore] public Subject? Subject { get; set; }

        [ForeignKey("Semester")]
        public int FK_SemesterID { get; set; }
        [JsonIgnore] public Semester? Semester { get; set; }

        [ForeignKey("GradeType")]
        public int FK_GradeTypeID { get; set; }
        [JsonIgnore] public GradeType? GradeType { get; set; }

        [ForeignKey("SchoolYear")]
        public int FK_SchoolYearID { get; set; }
        [JsonIgnore] public SchoolYear? SchoolYear { get; set; }

        [ForeignKey("ClassType")]
        public int? FK_ClassTypeID { get; set; } // Nullable based on diagram
        [JsonIgnore] public ClassType? ClassType { get; set; }

        public double Score { get; set; }

        [StringLength(255)]
        public string? Instance { get; set; } // E.g., "Midterm", "Final", "Quiz 1"
    }
}
