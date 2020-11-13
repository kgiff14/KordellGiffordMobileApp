using KordellGiffordMobileApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KordellGiffordMobileApp.ViewModels
{
    public class CoursesHomeViewModel : BaseViewModel
    {
        public ObservableCollection<TermModel> Terms = new ObservableCollection<TermModel>();
        public ObservableCollection<CourseModel> TermCourses = new ObservableCollection<CourseModel>();
        public CourseModel course = new CourseModel();
        public TermModel term = new TermModel();



        public ObservableCollection<TermModel> GetAllTerms()
        {
            try
            {
                Terms.Clear();
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    Terms = new ObservableCollection<TermModel>(conn.Table<TermModel>().ToList());

                    return Terms;
                }
            }
            catch
            {
                Terms.Clear();
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<TermModel>();
                    term.TermName = "Fall 2020";
                    term.TermStartDate = Convert.ToDateTime("10/1/2020");
                    term.TermEndDate = Convert.ToDateTime("2/1/2021");
                    conn.Insert(term);
                    Terms = new ObservableCollection<TermModel>(conn.Table<TermModel>().ToList());

                    return Terms;
                }
            }
        }

        public ObservableCollection<CourseModel> GetTermCourses(int termId)
        {
            try
            {
                TermCourses.Clear();
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    var results = conn.Table<CourseModel>().ToList();
                    TermCourses = new ObservableCollection<CourseModel>(results.Where(x => x.TermId == termId).ToList());

                    return TermCourses;
                }
            }
            catch
            {
                TermCourses.Clear();
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<CourseModel>();
                    course.CourseName = "Math I";
                    course.CourseStartDate = Convert.ToDateTime("11/15/2021");
                    course.CourseEndDate = Convert.ToDateTime("1/1/2021");
                    course.Status = "In Progress";
                    course.TermId = 0;
                    course.InstructorName = "Kordell Gifford";
                    course.InstructorPhone = "123-123-1234";
                    course.InstructorEmail = "kgiff14@gmail.com";
                    course.OAName = "Math I OA";
                    course.OADetails = "Pen and paper test of about 50 questions.";
                    course.OADate = Convert.ToDateTime("11/17/2021");
                    course.PAName = "Math I PA";
                    course.PADetails = "Whiteboard test creating and solving basic algebra.";
                    course.PADate = Convert.ToDateTime("12/1/2021");
                    course.Notes = "Remeber to read page 352. Also read chapter 12!!";
                    var results = conn.Table<CourseModel>().ToList();
                    TermCourses = new ObservableCollection<CourseModel>(results.Where(x => x.TermId == termId).ToList());

                    return TermCourses;
                }
            }
        }
    }
}
