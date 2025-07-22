namespace DuAnThucTap.Irepository
{
    public interface ITeacherConcurrentSubjectService
    {
        Task<IEnumerable<TeacherConcurrentSubject>> GetAllAsync();
        Task<TeacherConcurrentSubject?> GetByIdAsync(int teacherId, int subjectId, int schoolYearId);
        Task<TeacherConcurrentSubject> CreateAsync(TeacherConcurrentSubjectDto dto);
        Task<bool> UpdateAsync(int teacherId, int subjectId, int schoolYearId, TeacherConcurrentSubjectDto dto);
        Task<bool> DeleteAsync(int teacherId, int subjectId, int schoolYearId);
    }
}
