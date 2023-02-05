using System;
using System.Collections.Generic;

namespace TestTask.DAL
{
    public class OrganizationsEmployeeGroup
    {
        public string Fio { get; set; } = null!;
        public string OrganizationName { get; set; } = null!;
        public int EmployeeId { get; set; }
        public int? StudyGroupId { get; set; }
    }
}
