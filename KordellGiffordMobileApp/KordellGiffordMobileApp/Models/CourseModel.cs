using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KordellGiffordMobileApp.Models
{
    public class CourseModel
    {
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public bool Active { get; set; }
        public int TermId { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string OAName { get; set; }
        public string OADetails { get; set; }
        public DateTime OADate { get; set; }
        public string PAName { get; set; }
        public string PADetails { get; set; }
        public DateTime PADate { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string FullCourseDates
        {
            get
            {
                return string.Format("{0} - {1}", CourseStartDate.ToString("MMMM dd, yyyy"), CourseEndDate.ToString("MMMM dd, yyyy"));
            }
        }


        public CourseModel () { }
    }
}
