using System;
using System.Collections.Generic;

namespace TestTask.DAL
{
    public partial class Employee
    {
        public Employee()
        {
            StudyGroupsEmployees = new HashSet<StudyGroupsEmployee>();
        }

        public int Id { get; set; }
        public string Fio { get; set; } = null!;
        public int? OrganizationId { get; set; }

        public virtual Organization? Organization { get; set; }
        public virtual ICollection<StudyGroupsEmployee> StudyGroupsEmployees { get; set; }
    }
}
