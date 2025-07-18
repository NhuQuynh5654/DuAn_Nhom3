using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DuAnThucTap.Migrations
{
    public partial class Demo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campuses",
                columns: table => new
                {
                    CampusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CampusName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ImageURL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ContactPersonName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ContactPersonMobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ContactPersonEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campuses", x => x.CampusID);
                });

            migrationBuilder.CreateTable(
                name: "ClassTypes",
                columns: table => new
                {
                    ClassTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassTypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTypes", x => x.ClassTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "GradeTypes",
                columns: table => new
                {
                    GradeTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GradeTypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WeightingFactor = table.Column<double>(type: "double precision", nullable: false),
                    MinInstancesSemester1 = table.Column<double>(type: "double precision", nullable: false),
                    MinInstancesSemester2 = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeTypes", x => x.GradeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolInformation",
                columns: table => new
                {
                    SchoolInfoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchoolName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    StandardCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Ward = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SchoolType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FaxNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    EstablishmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TrainingModel = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WebsiteURL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PrincipalName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PrincipalPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LogoURL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolInformation", x => x.SchoolInfoID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolYears",
                columns: table => new
                {
                    SchoolYearID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchoolYearName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartYear = table.Column<int>(type: "integer", nullable: false),
                    EndYear = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYears", x => x.SchoolYearID);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTypes",
                columns: table => new
                {
                    SubjectTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectTypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTypes", x => x.SubjectTypeID);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentLeaders",
                columns: table => new
                {
                    DepartmentLeaderID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FK_DepartmentID = table.Column<int>(type: "integer", nullable: false),
                    DepartmentLeaderName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentLeaders", x => x.DepartmentLeaderID);
                    table.ForeignKey(
                        name: "FK_DepartmentLeaders_Departments_FK_DepartmentID",
                        column: x => x.FK_DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradeLevels",
                columns: table => new
                {
                    GradeLevelID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GradeLevelName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GradeCodeLevel = table.Column<int>(type: "integer", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FK_SchoolYearID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeLevels", x => x.GradeLevelID);
                    table.ForeignKey(
                        name: "FK_GradeLevels_SchoolYears_FK_SchoolYearID",
                        column: x => x.FK_SchoolYearID,
                        principalTable: "SchoolYears",
                        principalColumn: "SchoolYearID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    SemesterID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SemesterName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FK_SchoolYearID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.SemesterID);
                    table.ForeignKey(
                        name: "FK_Semesters_SchoolYears_FK_SchoolYearID",
                        column: x => x.FK_SchoolYearID,
                        principalTable: "SchoolYears",
                        principalColumn: "SchoolYearID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FK_SubjectTypeID = table.Column<int>(type: "integer", nullable: false),
                    DefaultPeriodsSem1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DefaultPeriodsSem2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FK_SchoolYearID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_Subjects_SchoolYears_FK_SchoolYearID",
                        column: x => x.FK_SchoolYearID,
                        principalTable: "SchoolYears",
                        principalColumn: "SchoolYearID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Subjects_SubjectTypes_FK_SubjectTypeID",
                        column: x => x.FK_SubjectTypeID,
                        principalTable: "SubjectTypes",
                        principalColumn: "SubjectTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlockLeaders",
                columns: table => new
                {
                    BlockLeaderID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FK_GradeLevelID = table.Column<int>(type: "integer", nullable: false),
                    FK_SchoolYearID = table.Column<int>(type: "integer", nullable: false),
                    BlockLeaderName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockLeaders", x => x.BlockLeaderID);
                    table.ForeignKey(
                        name: "FK_BlockLeaders_GradeLevels_FK_GradeLevelID",
                        column: x => x.FK_GradeLevelID,
                        principalTable: "GradeLevels",
                        principalColumn: "GradeLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlockLeaders_SchoolYears_FK_SchoolYearID",
                        column: x => x.FK_SchoolYearID,
                        principalTable: "SchoolYears",
                        principalColumn: "SchoolYearID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MaxStudents = table.Column<int>(type: "integer", nullable: false),
                    FK_SchoolYearID = table.Column<int>(type: "integer", nullable: false),
                    FK_GradeLevelID = table.Column<int>(type: "integer", nullable: false),
                    FK_ClassTypeID = table.Column<int>(type: "integer", nullable: false),
                    FK_TeacherID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassID);
                    table.ForeignKey(
                        name: "FK_Classes_ClassTypes_FK_ClassTypeID",
                        column: x => x.FK_ClassTypeID,
                        principalTable: "ClassTypes",
                        principalColumn: "ClassTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_GradeLevels_FK_GradeLevelID",
                        column: x => x.FK_GradeLevelID,
                        principalTable: "GradeLevels",
                        principalColumn: "GradeLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_SchoolYears_FK_SchoolYearID",
                        column: x => x.FK_SchoolYearID,
                        principalTable: "SchoolYears",
                        principalColumn: "SchoolYearID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopicLists",
                columns: table => new
                {
                    TopicID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TopicName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    TeachingStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TeachingEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FK_SemesterID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicLists", x => x.TopicID);
                    table.ForeignKey(
                        name: "FK_TopicLists_Semesters_FK_SemesterID",
                        column: x => x.FK_SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "SemesterID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentID = table.Column<int>(type: "integer", nullable: false),
                    FK_SubjectID = table.Column<int>(type: "integer", nullable: false),
                    FK_SemesterID = table.Column<int>(type: "integer", nullable: false),
                    FK_GradeTypeID = table.Column<int>(type: "integer", nullable: false),
                    FK_SchoolYearID = table.Column<int>(type: "integer", nullable: false),
                    FK_ClassTypeID = table.Column<int>(type: "integer", nullable: true),
                    Score = table.Column<double>(type: "double precision", nullable: false),
                    Instance = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeID);
                    table.ForeignKey(
                        name: "FK_Grades_ClassTypes_FK_ClassTypeID",
                        column: x => x.FK_ClassTypeID,
                        principalTable: "ClassTypes",
                        principalColumn: "ClassTypeID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Grades_GradeTypes_FK_GradeTypeID",
                        column: x => x.FK_GradeTypeID,
                        principalTable: "GradeTypes",
                        principalColumn: "GradeTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_SchoolYears_FK_SchoolYearID",
                        column: x => x.FK_SchoolYearID,
                        principalTable: "SchoolYears",
                        principalColumn: "SchoolYearID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Semesters_FK_SemesterID",
                        column: x => x.FK_SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "SemesterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Subjects_FK_SubjectID",
                        column: x => x.FK_SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingAssignments",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FK_ClassID = table.Column<int>(type: "integer", nullable: false),
                    FK_SubjectID = table.Column<int>(type: "integer", nullable: false),
                    FK_ClassTypeID = table.Column<int>(type: "integer", nullable: true),
                    FK_SchoolYearID = table.Column<int>(type: "integer", nullable: false),
                    TeachingStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TeachingEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingAssignments", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_Classes_FK_ClassID",
                        column: x => x.FK_ClassID,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_ClassTypes_FK_ClassTypeID",
                        column: x => x.FK_ClassTypeID,
                        principalTable: "ClassTypes",
                        principalColumn: "ClassTypeID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_SchoolYears_FK_SchoolYearID",
                        column: x => x.FK_SchoolYearID,
                        principalTable: "SchoolYears",
                        principalColumn: "SchoolYearID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_Subjects_FK_SubjectID",
                        column: x => x.FK_SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockLeaders_FK_GradeLevelID",
                table: "BlockLeaders",
                column: "FK_GradeLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_BlockLeaders_FK_SchoolYearID",
                table: "BlockLeaders",
                column: "FK_SchoolYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_FK_ClassTypeID",
                table: "Classes",
                column: "FK_ClassTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_FK_GradeLevelID",
                table: "Classes",
                column: "FK_GradeLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_FK_SchoolYearID",
                table: "Classes",
                column: "FK_SchoolYearID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLeaders_FK_DepartmentID",
                table: "DepartmentLeaders",
                column: "FK_DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_GradeLevels_FK_SchoolYearID",
                table: "GradeLevels",
                column: "FK_SchoolYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_ClassTypeID",
                table: "Grades",
                column: "FK_ClassTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_GradeTypeID",
                table: "Grades",
                column: "FK_GradeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_SchoolYearID",
                table: "Grades",
                column: "FK_SchoolYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_SemesterID",
                table: "Grades",
                column: "FK_SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_SubjectID",
                table: "Grades",
                column: "FK_SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_FK_SchoolYearID",
                table: "Semesters",
                column: "FK_SchoolYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FK_SchoolYearID",
                table: "Subjects",
                column: "FK_SchoolYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FK_SubjectTypeID",
                table: "Subjects",
                column: "FK_SubjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_FK_ClassID",
                table: "TeachingAssignments",
                column: "FK_ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_FK_ClassTypeID",
                table: "TeachingAssignments",
                column: "FK_ClassTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_FK_SchoolYearID",
                table: "TeachingAssignments",
                column: "FK_SchoolYearID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_FK_SubjectID",
                table: "TeachingAssignments",
                column: "FK_SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TopicLists_FK_SemesterID",
                table: "TopicLists",
                column: "FK_SemesterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockLeaders");

            migrationBuilder.DropTable(
                name: "Campuses");

            migrationBuilder.DropTable(
                name: "DepartmentLeaders");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "SchoolInformation");

            migrationBuilder.DropTable(
                name: "TeachingAssignments");

            migrationBuilder.DropTable(
                name: "TopicLists");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "GradeTypes");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "ClassTypes");

            migrationBuilder.DropTable(
                name: "GradeLevels");

            migrationBuilder.DropTable(
                name: "SubjectTypes");

            migrationBuilder.DropTable(
                name: "SchoolYears");
        }
    }
}
