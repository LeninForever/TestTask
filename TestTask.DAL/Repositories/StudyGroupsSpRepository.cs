using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Repositories;

public class StudyGroupsSpRepository : IStudyGroupsRepository
{
    private readonly StudydbContext _dbContext;


    public StudyGroupsSpRepository(StudydbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<GroupsView>> GetGroupsWithTeachers()
    {
        var groups = _dbContext.GroupsViews.FromSqlRaw("SelectGroupsTeachersAndStudentCount").ToListAsync();
        return groups;
    }


    public int AddNewGroup(StudyGroup studyGroup)
    {
        var groupNameParam = new SqlParameter("@groupName", studyGroup.StudyGroupName);
        var teacherIdParam = new SqlParameter("@teacherId", studyGroup.TeacherId);
        return _dbContext.Database.ExecuteSqlRaw("AddNewStudyGroup @groupName, @teacherId", groupNameParam,
            teacherIdParam);
    }

    public int UpdateGroupName(int groupId, string newStudyGroupName)
    {
        var groupIdParam = new SqlParameter("@updatedGroupId", groupId);
        var newStudyGroupNameParam = new SqlParameter("@newStudyGroupName", newStudyGroupName);
        return _dbContext.Database.ExecuteSqlRaw("UpdateGroupName @updatedGroupId, @newStudyGroupName", groupIdParam,
            newStudyGroupNameParam);
    }

    public int RemoveEmployeeFromStudyGroup(int employeeId, int studyGroupId)
    {
        var employeeIdParam = new SqlParameter("@employeeId", employeeId);
        var studyGroupIdParam = new SqlParameter("@studyGroupId", studyGroupId);
        return _dbContext.Database.ExecuteSqlRaw("RemoveEmployeeFromStudyGroup @employeeId, @studyGroupId", employeeIdParam,
            studyGroupIdParam);
    }

    public int AddEmployeeToGroup(int employeeId, int studyGroupId)
    {
        var employeeIdParam = new SqlParameter("@employeeId", employeeId);
        var studyGroupIdParam = new SqlParameter("@studyGroupId", studyGroupId);
        return _dbContext.Database.ExecuteSqlRaw("AddEmployeeToStudyGroup @employeeId, @studyGroupId", employeeIdParam,
            studyGroupIdParam);
    }

    public Task<List<OrganizationsEmployeeGroup>> GetEmployeeOrganization(int groupId)
    {
        var groupIdParam = new SqlParameter("@studyGroupId", groupId);
        var groups = _dbContext.OrganizationsEmployeeGroups
            .FromSqlRaw("SelectEmployeesAndOrgNameByGroupId @studyGroupId", groupIdParam).ToListAsync();
        return groups;
    }
}