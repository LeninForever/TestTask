namespace TestTask.DAL.Interfaces;

public interface ITeachersRepository
{
    Task<List<Teacher>> GetTeachers();

    Task<StudyGroupTeacher?> GetTeacherByGroupId(int id);

    public Task<List<Organization>> GetOrganizationsAttachedToTeacher(int teacherId);
}