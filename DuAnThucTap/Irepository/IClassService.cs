using DuAnThucTap.Model;
using DuAnThucTap.DTO;

namespace DuAnThucTap.Irepository
{
    public interface IClassService
    {
        Task<IEnumerable<ClassInfoDto>> GetAllClass();
        Task<IEnumerable<ClassInfoDto>> GetInfoClass(int id);
        Task<ClassInfoDto?> GetByIdAsync(int id);
        Task<Class> CreateAsync(Class classEntity);
        Task<bool> UpdateAsync(int id, Class classEntity);
        Task<bool> DeleteAsync(int id);
    }
}
