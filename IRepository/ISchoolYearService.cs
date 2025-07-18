using DuAnThucTap.Models;

namespace DuAnThucTap.IRepository
{
    public interface ISchoolYearService
    {
        Task<IEnumerable<SchoolYear>>? GetAllSchoolYearsAsync();
        Task<SchoolYear>? GetSchoolYearByIdAsync(int id);
        Task<SchoolYear> CreateSchoolYearAsync(SchoolYear newSchoolYear, bool inheritFromPreviousYear);
        Task UpdateSchoolYearAsync(SchoolYear schoolYear);
        Task DeleteSchoolYearAsync(int id);
    }
}
