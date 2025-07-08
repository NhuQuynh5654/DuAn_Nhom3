using DuAnThucTap.Data;
using DuAnThucTap.DTO;
using DuAnThucTap.Irepository;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap.Service
{
    public class ClassService : IClassService
    {
        private readonly ApplicationDbContext _context;
        public ClassService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClassInfoDto>> GetAllClass()
        {
            return await _context.Classes
                .Include(c => c.Teacher)
                .Select(c => new ClassInfoDto
                {
                    Classid = c.Classid,
                    Classname = c.Classname,
                    TeacherFullname = c.Teacher != null ? c.Teacher.Fullname : null
                })
                .ToListAsync();
        }

        //lấy thông tin chi tiết của lớp học (6.3)
        public async Task<IEnumerable<ClassInfoDto>> GetInfoClass(int id)
        {
            return await _context.Classes
                .Include(c => c.Subjects)
                .Include(c => c.Teacher)
                .Include(c => c.Classtype)
                .Include(c => c.Schoolyear)
                .Include(c => c.Classid == id)
                .Select(c => new ClassInfoDto
                {
                    Classid = c.Classid,
                    Classname = c.Classname,
                    SubjectCount = c.Subjects.Count,
                    TeacherFullname = c.Teacher != null ? c.Teacher.Fullname : null,
                    ClassTypeName = c.Classtype != null ? c.Classtype.Classtypename : null,
                    SchoolYear = c.Schoolyear != null ? $"{c.Schoolyear.Startyear}-{c.Schoolyear.Endyear}" : null
                })
                .ToListAsync();
        }

        public async Task<ClassInfoDto?> GetByIdAsync(int id)
        {
            return await _context.Classes
                .Include(c => c.Teacher)
                .Where(c => c.Classid == id)
                .Select(c => new ClassInfoDto
                {
                    Classid = c.Classid,
                    Classname = c.Classname,
                    TeacherFullname = c.Teacher != null ? c.Teacher.Fullname : null
                })
                .FirstOrDefaultAsync();
        }


        public async Task<Class> CreateAsync(Class classEntity)
        {
            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();
            return classEntity;
        }

        public async Task<bool> UpdateAsync(int id, Class classEntity)
        {
            if (id != classEntity.Classid) return false;
            _context.Entry(classEntity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity == null) return false;

            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
