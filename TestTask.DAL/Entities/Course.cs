using System;
using System.Collections.Generic;

namespace TestTask.DAL
{
    public partial class Course
    {
        public Course()
        {
            StudyGroups = new HashSet<StudyGroup>();
        }

        public int Id { get; set; }
        public string CourseName { get; set; } = null!;

        public virtual ICollection<StudyGroup> StudyGroups { get; set; }
    }
}
