using DuAnThucTap.IRepository;
using DuAnThucTap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolYearController : ControllerBase
    {
        private readonly ISchoolYearService _schoolYearService;

        public SchoolYearController(ISchoolYearService schoolYearService)
        {
            _schoolYearService = schoolYearService;
        }

        /// <summary>
        /// Lấy tất cả các năm học.
        /// </summary>
        /// <returns>Danh sách các năm học.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolYear>>> GetAllSchoolYears()
        {
            var schoolYears = await _schoolYearService.GetAllSchoolYearsAsync();
            return Ok(schoolYears);
        }

        /// <summary>
        /// Lấy một năm học theo ID.
        /// </summary>
        /// <param name="id">ID của năm học.</param>
        /// <returns>Năm học tương ứng hoặc NotFound nếu không tìm thấy.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolYear>> GetSchoolYearById(int id)
        {
            var schoolYear = await _schoolYearService.GetSchoolYearByIdAsync(id);
            if (schoolYear == null)
            {
                return NotFound($"Không tìm thấy năm học với ID: {id}");
            }
            return Ok(schoolYear);
        }

        /// <summary>
        /// Tạo một năm học mới, có tùy chọn kế thừa dữ liệu từ năm học trước.
        /// </summary>
        /// <param name="model">Dữ liệu năm học mới và tùy chọn kế thừa.</param>
        /// <returns>Năm học đã được tạo.</returns>
        [HttpPost]
        public async Task<ActionResult<SchoolYear>> CreateSchoolYear([FromBody] SchoolYearCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSchoolYear = new SchoolYear
            {
                SchoolYearName = model.SchoolYearName,
                StartYear = model.StartYear,
                EndYear = model.EndYear,
                //StartDate = model.StartDate, // Sử dụng StartDate từ input
                //EndDate = model.EndDate,     // Sử dụng EndDate từ input
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var createdSchoolYear = await _schoolYearService.CreateSchoolYearAsync(newSchoolYear, model.InheritFromPreviousYear);
            return CreatedAtAction(nameof(GetSchoolYearById), new { id = createdSchoolYear.SchoolYearID }, createdSchoolYear);
        }

        /// <summary>
        /// Cập nhật thông tin một năm học hiện có.
        /// </summary>
        /// <param name="id">ID của năm học cần cập nhật.</param>
        /// <param name="model">Dữ liệu cập nhật cho năm học.</param>
        /// <returns>NoContent nếu thành công, NotFound nếu không tìm thấy, hoặc BadRequest nếu dữ liệu không hợp lệ.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchoolYear(int id, [FromBody] SchoolYearUpdateInputModel model)
        {
            if (id != model.SchoolYearID)
            {
                return BadRequest("ID trong URL không khớp với ID trong body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSchoolYear = await _schoolYearService.GetSchoolYearByIdAsync(id);
            if (existingSchoolYear == null)
            {
                return NotFound($"Không tìm thấy năm học với ID: {id}");
            }

            // Cập nhật các thuộc tính
            existingSchoolYear.SchoolYearName = model.SchoolYearName;
            existingSchoolYear.StartYear = model.StartYear;
            existingSchoolYear.EndYear = model.EndYear;
            //existingSchoolYear.StartDate = model.StartDate;
            //existingSchoolYear.EndDate = model.EndDate;
            existingSchoolYear.UpdateAt = DateTime.UtcNow;

            await _schoolYearService.UpdateSchoolYearAsync(existingSchoolYear);
            return NoContent(); // Trả về 204 No Content cho PUT thành công
        }

        /// <summary>
        /// Xóa một năm học theo ID.
        /// </summary>
        /// <param name="id">ID của năm học cần xóa.</param>
        /// <returns>NoContent nếu thành công, hoặc NotFound nếu không tìm thấy.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolYear(int id)
        {
            var schoolYear = await _schoolYearService.GetSchoolYearByIdAsync(id);
            if (schoolYear == null)
            {
                return NotFound($"Không tìm thấy năm học với ID: {id}");
            }

            await _schoolYearService.DeleteSchoolYearAsync(id);
            return NoContent(); // Trả về 204 No Content cho DELETE thành công
        }
    }

    // DTO (Data Transfer Object) cho việc tạo năm học mới
    public class SchoolYearCreateInputModel
    {
        [Required(ErrorMessage = "Tên năm học là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên năm học không được vượt quá 50 ký tự.")]
        public string? SchoolYearName { get; set; }

        [Required(ErrorMessage = "Năm bắt đầu là bắt buộc.")]
        [Range(1900, 2100, ErrorMessage = "Năm bắt đầu phải nằm trong khoảng từ 1900 đến 2100.")]
        public int StartYear { get; set; }

        [Required(ErrorMessage = "Năm kết thúc là bắt buộc.")]
        [Range(1900, 2100, ErrorMessage = "Năm kết thúc phải nằm trong khoảng từ 1900 đến 2100.")]
        public int EndYear { get; set; }

        //[Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        //public DateTime? StartDate { get; set; }

        //[Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
        //public DateTime? EndDate { get; set; }

        public bool InheritFromPreviousYear { get; set; }
    }

    // DTO cho việc cập nhật năm học
    public class SchoolYearUpdateInputModel : SchoolYearCreateInputModel
    {
        [Required(ErrorMessage = "ID năm học là bắt buộc.")]
        public int SchoolYearID { get; set; }
    }
}

