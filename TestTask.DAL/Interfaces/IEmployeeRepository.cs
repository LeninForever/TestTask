namespace TestTask.DAL.Interfaces;

public interface IEmployeeRepository
{
    public Task<List<Employee>> GetEmployeesAttachedToOrganization(int orgId, int studyGroupId);
}