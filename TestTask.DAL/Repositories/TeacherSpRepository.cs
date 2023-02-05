using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;

public class TeacherSpRepository : ITeachersRepository
{
    private readonly StudydbContext _studydbContext;

    public TeacherSpRepository(StudydbContext studydbContext)
    {
        _studydbContext = studydbContext;
    }

    public async Task<List<Teacher>> GetTeachers()
    {
        return await _studydbContext.Teachers.FromSqlRaw("SelectTeachers").ToListAsync();
    }

    public async Task<StudyGroupTeacher?> GetTeacherByGroupId(int groupId)
    {
        var groupIdParam = new SqlParameter("@groupId", groupId);
        var list = await _studydbContext.StudyGroupTeachers
            .FromSqlRaw("GetGroupNameAndGroupTeacher @groupId", groupIdParam)
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<List<Organization>> GetOrganizationsAttachedToTeacher(int teacherId)
    {
        var teacherIdParam = new SqlParameter("@teacherId", teacherId);
        return await _studydbContext.Organizations
            .FromSqlRaw("GetOrganizationsAttachedToTeacher @teacherId", teacherIdParam)
            .ToListAsync();
    }
}