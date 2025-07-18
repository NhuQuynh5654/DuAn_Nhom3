using DuAnThucTap.Data;
using DuAnThucTap.IRepository;
using DuAnThucTap.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap.Service
{
    public class SchoolYearService : ISchoolYearService
    {
        private readonly SchoolDbContext _context;

        // Service giờ đây trực tiếp nhận SchoolDbContext
        public SchoolYearService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SchoolYear>> GetAllSchoolYearsAsync()
        {
            return await _context!.SchoolYears!.ToListAsync();
        }

        public async Task<SchoolYear> GetSchoolYearByIdAsync(int id)
        {
            return await _context.SchoolYears!.FindAsync(id) ?? throw new KeyNotFoundException($"Năm học với ID {id} không tồn tại.");
        }

        public async Task<SchoolYear> CreateSchoolYearAsync(SchoolYear newSchoolYear, bool inheritFromPreviousYear)
        {
            // 1. Thêm năm học mới
            await _context!.SchoolYears!.AddAsync(newSchoolYear);
            await _context.SaveChangesAsync(); // Lưu để có SchoolYearID

            if (inheritFromPreviousYear)
            {
                // 2. Tìm năm học trước đó
                var previousSchoolYear = await _context.SchoolYears
                    .OrderByDescending(sy => sy.StartYear)
                    .ThenByDescending(sy => sy.SchoolYearID) // Để lấy năm học gần nhất nếu StartYear giống nhau
                    .FirstOrDefaultAsync(sy => sy.SchoolYearID != newSchoolYear.SchoolYearID);

                if (previousSchoolYear != null)
                {
                    await InheritDataFromPreviousYear(newSchoolYear, previousSchoolYear);
                }
            }

            return newSchoolYear;
        }

        public async Task UpdateSchoolYearAsync(SchoolYear schoolYear)
        {
            _context!.SchoolYears!.Update(schoolYear);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSchoolYearAsync(int id)
        {
            var schoolYear = await _context.SchoolYears!.FindAsync(id);
            if (schoolYear != null)
            {
                _context.SchoolYears.Remove(schoolYear);
                await _context.SaveChangesAsync();
            }
        }

        private async Task InheritDataFromPreviousYear(SchoolYear newSchoolYear, SchoolYear previousSchoolYear)
        {
            Console.WriteLine($"Kế thừa dữ liệu từ năm học trước: {previousSchoolYear.SchoolYearName} sang {newSchoolYear.SchoolYearName}");

            // Kế thừa Semesters
            await InheritSemesters(newSchoolYear, previousSchoolYear);

            // Kế thừa Classes
            await InheritClasses(newSchoolYear, previousSchoolYear);

            // Kế thừa Subjects (nếu có FK_SchoolYearID cụ thể)
            await InheritSubjects(newSchoolYear, previousSchoolYear);

            // Kế thừa GradeLevels (nếu có FK_SchoolYearID cụ thể)
            await InheritGradeLevels(newSchoolYear, previousSchoolYear);

            // Kế thừa BlockLeaders
            //await InheritBlockLeaders(newSchoolYear, previousSchoolYear);

            // Lưu tất cả các thay đổi
            await _context.SaveChangesAsync();
            Console.WriteLine("Đã hoàn tất kế thừa dữ liệu.");
        }

        private async Task InheritSemesters(SchoolYear newSchoolYear, SchoolYear previousSchoolYear)
        {
            var previousSemesters = await _context!.Semesters!
                .Where(s => s.FK_SchoolYearID == previousSchoolYear.SchoolYearID)
                .ToListAsync();

            foreach (var prevSem in previousSemesters)
            {
                var newSemester = new Semester
                {
                    SemesterName = prevSem.SemesterName,
                    // Điều chỉnh ngày tháng dựa trên năm học mới
                    StartDate = prevSem.StartDate?.AddYears(newSchoolYear.StartYear - previousSchoolYear.StartYear),
                    EndDate = prevSem.EndDate?.AddYears(newSchoolYear.EndYear - previousSchoolYear.EndYear),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    FK_SchoolYearID = newSchoolYear.SchoolYearID
                };
                await _context.Semesters!.AddAsync(newSemester);
            }
            Console.WriteLine($"Đã kế thừa {previousSemesters.Count} học kỳ.");
        }

        private async Task InheritClasses(SchoolYear newSchoolYear, SchoolYear previousSchoolYear)
        {
            var previousClasses = await _context.Classes!
                .Where(c => c.FK_SchoolYearID == previousSchoolYear.SchoolYearID)
                .ToListAsync();

            foreach (var prevClass in previousClasses)
            {
                var newClass = new Class
                {
                    ClassName = prevClass.ClassName,
                    Description = prevClass.Description,
                    MaxStudents = prevClass.MaxStudents,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    FK_SchoolYearID = newSchoolYear.SchoolYearID,
                    FK_GradeLevelID = prevClass.FK_GradeLevelID, // Giữ nguyên GradeLevel
                    FK_ClassTypeID = prevClass.FK_ClassTypeID,   // Giữ nguyên ClassType
                    FK_TeacherID = null // Giáo viên có thể thay đổi, đặt null để phân công lại
                };
                await _context.Classes!.AddAsync(newClass);
            }
            Console.WriteLine($"Đã kế thừa {previousClasses.Count} lớp học.");
        }

        private async Task InheritSubjects(SchoolYear newSchoolYear, SchoolYear previousSchoolYear)
        {
            // Chỉ sao chép các môn học được liên kết cụ thể với năm học trước
            var previousYearSpecificSubjects = await _context!.Subjects!
                .Where(s => s.FK_SchoolYearID == previousSchoolYear.SchoolYearID)
                .ToListAsync();

            foreach (var prevSub in previousYearSpecificSubjects)
            {
                var newSubject = new Subject
                {
                    SubjectName = prevSub.SubjectName,
                    FK_SubjectTypeID = prevSub.FK_SubjectTypeID,
                    DefaultPeriodsSem1 = prevSub.DefaultPeriodsSem1,
                    DefaultPeriodsSem2 = prevSub.DefaultPeriodsSem2,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    FK_SchoolYearID = newSchoolYear.SchoolYearID // Liên kết với năm học mới
                };
                await _context.Subjects!.AddAsync(newSubject);
            }
            Console.WriteLine($"Đã kế thừa {previousYearSpecificSubjects.Count} môn học cụ thể theo năm.");
        }

        private async Task InheritGradeLevels(SchoolYear newSchoolYear, SchoolYear previousSchoolYear)
        {
            // Chỉ sao chép các cấp độ lớp được liên kết cụ thể với năm học trước
            var previousYearSpecificGradeLevels = await _context.GradeLevels!
                .Where(gl => gl.FK_SchoolYearID == previousSchoolYear.SchoolYearID)
                .ToListAsync();

            foreach (var prevGl in previousYearSpecificGradeLevels)
            {
                var newGradeLevel = new GradeLevel
                {
                    GradeLevelName = prevGl.GradeLevelName,
                    GradeCodeLevel = prevGl.GradeCodeLevel,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    FK_SchoolYearID = newSchoolYear.SchoolYearID // Liên kết với năm học mới
                };
                await _context.GradeLevels!.AddAsync(newGradeLevel);
            }
            Console.WriteLine($"Đã kế thừa {previousYearSpecificGradeLevels.Count} cấp độ lớp cụ thể theo năm.");
        }

        //private async Task InheritBlockLeaders(SchoolYear newSchoolYear, SchoolYear previousSchoolYear)
        //{
        //    var previousBlockLeaders = await _context.BlockLeaders
        //        .Where(bl => bl.FK_SchoolYearID == previousSchoolYear.SchoolYearID)
        //        .ToListAsync();

        //    foreach (var prevBl in previousBlockLeaders)
        //    {
        //        var newBlockLeader = new BlockLeader
        //        {
        //            BlockLeaderName = prevBl.BlockLeaderName,
        //            StartDate = newSchoolYear.StartDate, // Đặt ngày bắt đầu theo năm học mới
        //            EndDate = newSchoolYear.EndDate,     // Đặt ngày kết thúc theo năm học mới
        //            UpdateAt = DateTime.UtcNow,
        //            FK_GradeLevelID = prevBl.FK_GradeLevelID, // Giữ nguyên GradeLevel
        //            FK_SchoolYearID = newSchoolYear.SchoolYearID
        //        };
        //        await _context.BlockLeaders.AddAsync(newBlockLeader);
        //    }
        //    Console.WriteLine($"Đã kế thừa {previousBlockLeaders.Count} trưởng khối.");
        //}
    }
}
