using System;
using System.Collections.Generic;

namespace TestTask.DAL
{
    public  class Teacher
    {
        public Teacher()
        {
            Organizations = new HashSet<Organization>();
            StudyGroups = new HashSet<StudyGroup>();
        }

        public int Id { get; set; }
        public string Fio { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<StudyGroup> StudyGroups { get; set; }
    }
}
