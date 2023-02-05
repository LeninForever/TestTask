using System;
using System.Collections.Generic;

namespace TestTask.DAL
{
    public partial class StudyGroupsEmployee
    {
        public int Id { get; set; }
        public int? StudyGroupId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual StudyGroup? StudyGroup { get; set; }
    }
}
