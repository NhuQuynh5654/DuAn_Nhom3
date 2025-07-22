using DuAnThucTap.Irepository;
using DuAnThucTap.Model;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherConcurrentSubjectController : ControllerBase
    {
        private readonly ITeacherConcurrentSubjectService _service;

        public TeacherConcurrentSubjectController(ITeacherConcurrentSubjectService service)
        {
            _service = service;
        }

        // GET: api/TeacherConcurrentSubject
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();

            var result = data.Select(tcs => new
            {
                TeacherID = tcs.TeacherID,
                SubjectID = tcs.SubjectID,
                SchoolYearID = tcs.SchoolYearID,
                TeacherName = tcs.Teacher?.Fullname,
                SubjectName = tcs.Subject?.Subjectname,
                SchoolYearName = tcs.SchoolYear?.Schoolyearname
            });
            return Ok(result);
        }

        // GET: api/TeacherConcurrentSubject/{teacherId}/{subjectId}/{schoolYearId}
        [HttpGet("{teacherId:int}/{subjectId:int}/{schoolYearId:int}")]
        public async Task<IActionResult> GetById(int teacherId, int subjectId, int schoolYearId)
        {
            var result = await _service.GetByIdAsync(teacherId, subjectId, schoolYearId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/TeacherConcurrentSubject
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherConcurrentSubjectDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new
            {
                teacherId = result.TeacherID,
                subjectId = result.SubjectID,
                schoolYearId = result.SchoolYearID
            }, result);
        }

        // PUT: api/TeacherConcurrentSubject/{teacherId}/{subjectId}/{schoolYearId}
        [HttpPut("{teacherId:int}/{subjectId:int}/{schoolYearId:int}")]
        public async Task<IActionResult> Update(int teacherId, int subjectId, int schoolYearId, [FromBody] TeacherConcurrentSubjectDto dto)
        {
            var success = await _service.UpdateAsync(teacherId, subjectId, schoolYearId, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/TeacherConcurrentSubject/{teacherId}/{subjectId}/{schoolYearId}
        [HttpDelete("{teacherId:int}/{subjectId:int}/{schoolYearId:int}")]
        public async Task<IActionResult> Delete(int teacherId, int subjectId, int schoolYearId)
        {
            var success = await _service.DeleteAsync(teacherId, subjectId, schoolYearId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
