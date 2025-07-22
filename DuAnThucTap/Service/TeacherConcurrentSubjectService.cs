using DuAnThucTap.Data;
using DuAnThucTap.Irepository;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap.Service
{
    public class TeacherConcurrentSubjectService : ITeacherConcurrentSubjectService
    {
        public ApplicationDbContext _context;
        public TeacherConcurrentSubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherConcurrentSubject> CreateAsync(TeacherConcurrentSubjectDto dto)
        {
            var entity = new TeacherConcurrentSubject
            {
                TeacherID = dto.TeacherID,
                SubjectID = dto.SubjectID,
                SchoolYearID = dto.SchoolYearID
            };

            _context.TeacherConcurrentSubjects.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int teacherId, int subjectId, int schoolYearId)
        {
            var entity = _context.TeacherConcurrentSubjects
                .FirstOrDefault(tcs => tcs.TeacherID == teacherId && 
                                       tcs.SubjectID == subjectId && 
                                       tcs.SchoolYearID == schoolYearId);
                
            if (entity == null)
                return false;

            _context.TeacherConcurrentSubjects.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TeacherConcurrentSubject>> GetAllAsync()
        {
            return await _context.TeacherConcurrentSubjects
                .Include(t => t.Subject)
                .Include(t => t.SchoolYear)
                .Include(t => t.Teacher)
                .OrderBy(t => t.Teacher.Fullname)
                .ToListAsync();
        }

        public Task<TeacherConcurrentSubject?> GetByIdAsync(int teacherId, int subjectId, int schoolYearId)
        {
            return _context.TeacherConcurrentSubjects
                .Include(t => t.Teacher)
                .Include(t => t.Subject)
                .Include(t => t.SchoolYear)
                .FirstOrDefaultAsync(tcs => tcs.TeacherID == teacherId && 
                                            tcs.SubjectID == subjectId && 
                                            tcs.SchoolYearID == schoolYearId);
        }

        public async Task<bool> UpdateAsync(int teacherId, int subjectId, int schoolYearId, TeacherConcurrentSubjectDto dto)
        {
            var entity = _context.TeacherConcurrentSubjects
                .FirstOrDefault(tcs => tcs.TeacherID == teacherId && 
                                       tcs.SubjectID == subjectId && 
                                       tcs.SchoolYearID == schoolYearId);

            entity.TeacherID = dto.TeacherID;
            entity.SubjectID = dto.SubjectID;
            entity.SchoolYearID = dto.SchoolYearID;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
