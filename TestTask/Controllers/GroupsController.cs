using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TestTask.DAL;
using TestTask.DAL.Interfaces;
using TestTask.DAL.Repositories;

namespace TestTask.Controllers;

public class GroupsController : Controller
{
    private readonly ILogger<GroupsController> _logger;
    private readonly IStudyGroupsRepository _groupsRepository;
    private readonly ITeachersRepository _teachersRepository;

    public GroupsController(ILogger<GroupsController> logger, IStudyGroupsRepository groupsRepository,
        ITeachersRepository teachersRepository)
    {
        _logger = logger;
        _groupsRepository = groupsRepository;
        _teachersRepository = teachersRepository;
    }

    public async Task<ViewResult> Index()
    {
        var groupsWithTeachers = await _groupsRepository.GetGroupsWithTeachers();
            return View(groupsWithTeachers);
    }

    [HttpGet]
    public async Task<IActionResult> EditGroup(int studyGroupId)
    {
        var queryResult = await _teachersRepository.GetTeacherByGroupId(studyGroupId);
        var studyGroupTeacher = queryResult ?? new StudyGroupTeacher
            { Fio = "Не найдено", StudyGroupId = 0, StudyGroupName = "Не найдено" };

        ViewBag.EmployeesOrganizations = await _groupsRepository.GetEmployeeOrganization(studyGroupId);

        return View(studyGroupTeacher);
    }

    [HttpPost]
    public IActionResult EditGroup(string studyGroupName)
    {
        if (ModelState.IsValid)
        {
            int groupId = int.Parse(HttpContext.Request.Query["studyGroupId"]);
            _groupsRepository.UpdateGroupName(groupId, studyGroupName);
            return RedirectToAction("Index");
        }
        throw new Exception("Некорретное название группы (длиннее допустимого или пустое)",
            new ValidationException(""));
    }

    [HttpGet]
    public async Task<IActionResult> AddNewGroup()
    {
        var teachers = await _teachersRepository.GetTeachers();
        ViewBag.Teachers = teachers;
        return View();
    }

    [HttpPost]
    public IActionResult AddNewGroup(StudyGroup studyGroup)
    {
        if (ModelState.IsValid)
        {
            _groupsRepository.AddNewGroup(studyGroup);
            return RedirectToAction("Index");
        }

        throw new Exception("Некорретное название группы (длиннее допустимого или пустое)",
            new ValidationException(""));
    }

    public IActionResult DeleteStudentFromGroup(int employeeId, int studyGroupId)
    {
        _groupsRepository.RemoveEmployeeFromStudyGroup(employeeId, studyGroupId);
        return RedirectToAction("EditGroup", new { studyGroupId = studyGroupId });
    }

    [HttpGet]
    public async Task<IActionResult> AddStudentToGroup([FromQuery] StudyGroupTeacher studyGroupTeacher)
    {
        if (ModelState.IsValid)
        {
            var organizations =
                await _teachersRepository.GetOrganizationsAttachedToTeacher(studyGroupTeacher.TeacherId);
            ViewBag.Organizations = organizations;
            ViewBag.StudyGroupTeacher = studyGroupTeacher;
            return View();
        }

        throw new ValidationException("Не выбран добавляемый студент!");
    }

    [HttpPost]
    public IActionResult AddStudentToGroup(int employeeId)
    {
        int studyGroupId = int.Parse(HttpContext.Request.Query["studyGroupId"]);
        _groupsRepository.AddEmployeeToGroup(employeeId, studyGroupId);
        return RedirectToAction("EditGroup", new { studyGroupId = studyGroupId });
    }
}