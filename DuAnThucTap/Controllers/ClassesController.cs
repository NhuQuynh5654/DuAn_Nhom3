using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnThucTap.Data;
using DuAnThucTap.Model;
using DuAnThucTap.Irepository;
using DuAnThucTap.DTO;

namespace DuAnThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;
        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassInfoDto>>> GetAllClasses()
        {
            var classes = await _classService.GetAllClass();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ClassInfoDto>>> GetClassInfo(int id)
        {
            var classInfos = await _classService.GetInfoClass(id);
            return Ok(classInfos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassInfoDto>> GetClassById(int id)
        {
            var classInfo = await _classService.GetByIdAsync(id);
            if (classInfo == null)
            {
                return NotFound();
            }
            return Ok(classInfo);
        }

        [HttpPost]
        public async Task<ActionResult<Class>> CreateClass(Class classEntity)
        {
            if (classEntity == null)
            {
                return BadRequest("Invalid class data.");
            }
            var createdClass = await _classService.CreateAsync(classEntity);
            return CreatedAtAction(nameof(GetClassById), new { id = createdClass.Classid }, createdClass);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(int id, Class classEntity)
        {
            if (id != classEntity.Classid)
            {
                return BadRequest("Class ID mismatch.");
            }
            var updated = await _classService.UpdateAsync(id, classEntity);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var deleted = await _classService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
