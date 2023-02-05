using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.DAL.Interfaces;

namespace TestTask.Controllers;

public class EmployeeController : Controller
{
    private IEmployeeRepository _employeeRepository;

    // GET
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpPost]
    public async Task<IActionResult> GetEmployeesAttachedToOrganization(int orgId, int studyGroupId)
    {
        var employees = await _employeeRepository.GetEmployeesAttachedToOrganization(orgId, studyGroupId);
        ViewBag.EmployeeSelectList = new SelectList(employees, "Id", "Fio");
        return PartialView();
    }
}