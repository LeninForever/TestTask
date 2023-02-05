using Microsoft.AspNetCore.Mvc;
using TestTask.DAL;
using TestTask.DAL.Interfaces;

namespace TestTask.Controllers;

public class TeachersController: Controller
{
    private readonly ITeachersRepository _teachersRepository;

    
    public TeachersController(ITeachersRepository teachersRepository)
    {
        _teachersRepository = teachersRepository;
    }

    public IActionResult GetTeachers()
    {
       
            var response = _teachersRepository.GetTeachers();
            return Ok(response);
    }

    public async Task<StudyGroupTeacher?> GetTeachersAttachedToOrganization(int organizationId)
    {
        return await _teachersRepository.GetTeacherByGroupId(organizationId);
    }
    
}