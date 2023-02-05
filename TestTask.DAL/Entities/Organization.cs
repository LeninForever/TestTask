using System;
using System.Collections.Generic;

namespace TestTask.DAL
{
    public partial class Organization
    {
        public Organization()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string OrganizationName { get; set; } = null!;
        public string Inn { get; set; } = null!;
        public int? TeacherId { get; set; }

        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
