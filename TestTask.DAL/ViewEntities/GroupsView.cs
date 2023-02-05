using System;
using System.Collections.Generic;

namespace TestTask.DAL
{
    public class GroupsView
    {
        public string StudyGroupName { get; set; } = null!;
        public string TeacherFio { get; set; } = null!;
        public int? EmployeeCount { get; set; }
        public int? StudyGroupId { get; set; }
    }
}
