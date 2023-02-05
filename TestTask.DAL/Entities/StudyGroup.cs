using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTask.DAL
{
    public partial class StudyGroup
    {
        public StudyGroup()
        {
            StudyGroupsEmployees = new HashSet<StudyGroupsEmployee>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string StudyGroupName { get; set; } = null!;
        public int? TeacherId { get; set; }
        public int? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<StudyGroupsEmployee> StudyGroupsEmployees { get; set; }
    }
}
