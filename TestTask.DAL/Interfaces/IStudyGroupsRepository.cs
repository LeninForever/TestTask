namespace TestTask.DAL.Interfaces;

public interface IStudyGroupsRepository
{
    public Task<List<GroupsView>> GetGroupsWithTeachers();

    public int AddNewGroup(StudyGroup studyGroup);

    public int UpdateGroupName(int groupId, string newStudyGroupName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public Task<List<OrganizationsEmployeeGroup>> GetEmployeeOrganization(int groupId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="studyGroupId"></param>
    /// <returns></returns>
    public int RemoveEmployeeFromStudyGroup(int employeeId, int studyGroupId);

    public int AddEmployeeToGroup(int employeeId, int studyGroupId);



}