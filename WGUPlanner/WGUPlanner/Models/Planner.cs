using SQLite;
using System;
using System.Collections.Generic;

namespace WGUPlanner.Models
{
    [Table("Planner")]
    public class Planner
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string TermTitle { get; set; }
        public DateTime TermStartDate { get; set; }
        public DateTime TermEndDate { get; set; }

        [MaxLength(50)]
        public bool TermNotification { get; set; }

        [MaxLength(50)]
        public string CourseTitle { get; set; }
        public string CourseStatus { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }

        [MaxLength(50)]
        public string CourseInstructorName { get; set; }

        [MaxLength(50)]
        public string CourseInstructorPhone { get; set; }

        [MaxLength(50)]
        public string CourseInstructorEmail { get; set; }

        [MaxLength(500)]
        public string CourseNotes { get; set; }

        public bool CourseNotification { get; set; }
        [MaxLength(50), Unique]
        public string AssessmentId { get; set; }
        [MaxLength(50)]
        public string AssessmentTitle { get; set; }
        public DateTime AssessmentStartDate { get; set; }
        public DateTime AssessmentEndDate { get; set; }

        [MaxLength(50)]
        public string AssessmentType { get; set; }

        [MaxLength(50)]
        public bool AssessmentNotification { get; set; }

    }

}