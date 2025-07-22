using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnThucTap.Data;
using DuAnThucTap.Model;

namespace DuAnThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            if (_context.Teachers == null)
                return NotFound();

            return await _context.Teachers.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            if (_context.Teachers == null)
                return NotFound();

            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
                return NotFound();

            return teacher;
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Teacherid)
                return BadRequest();

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // 🔄 Cập nhật liên kết môn học nếu cần (tuỳ logic bạn bổ sung)
                var concurrentSubjects = await _context.TeacherConcurrentSubjects
                    .Where(t => t.TeacherID == id)
                    .ToListAsync();

                foreach (var item in concurrentSubjects)
                {
                    // Giả sử cập nhật năm học chẳng hạn
                    item.SchoolYearID = item.SchoolYearID; // Giữ hoặc cập nhật theo nhu cầu
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            if (_context.Teachers == null)
                return Problem("Entity set 'ApplicationDbContext.Teachers' is null.");

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            // ✳️ Gắn thêm dữ liệu TeacherConcurrentSubject nếu có logic bổ sung
            // Ví dụ thêm một môn học mặc định:
            var defaultConcurrent = new TeacherConcurrentSubject
            {
                TeacherID = teacher.Teacherid,
                SubjectID = 1, // giả sử ID môn mặc định
                SchoolYearID = 1 // niên khóa mặc định
            };
            _context.TeacherConcurrentSubjects.Add(defaultConcurrent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Teacherid }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (_context.Teachers == null)
                return NotFound();

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
                return NotFound();

            // 🗑 Xóa liên kết TeacherConcurrentSubject
            var subjects = await _context.TeacherConcurrentSubjects
                .Where(t => t.TeacherID == id)
                .ToListAsync();

            _context.TeacherConcurrentSubjects.RemoveRange(subjects);

            // 🗑 Xóa giáo viên
            _context.Teachers.Remove(teacher);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return (_context.Teachers?.Any(e => e.Teacherid == id)).GetValueOrDefault();
        }
    }
}
