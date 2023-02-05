using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;

public class EmployeeSpRepository : IEmployeeRepository
{
    private readonly StudydbContext _studydbContext;

    public EmployeeSpRepository(StudydbContext studydbContext)
    {
        _studydbContext = studydbContext;
    }

    public async Task<List<Employee>> GetEmployeesAttachedToOrganization(int orgId, int studyGroupId)
    {
        var orgIdParam = new SqlParameter("@orgId", orgId);
        var studyGroupIdParam = new SqlParameter("@studyGroupId", studyGroupId);
        var response = await _studydbContext.Employees.FromSqlRaw("GetEmployeesAttachedToOrganization @orgId, @studyGroupId",
            orgIdParam, studyGroupIdParam).ToListAsync();

        return response;
    }
}