using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.DAL;

public class StudyGroupTeacher
{
    [Required]
    [StringLength(50)]
    public string StudyGroupName { get; set; }
    public string Fio { get; set; }
    public int StudyGroupId { get; set; }
    public int TeacherId { get; set; }
    
}