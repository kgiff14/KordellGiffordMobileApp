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



        public ObservableCollection<TermModel> GetAllTerms()
        {
            Terms.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                Terms = new ObservableCollection<TermModel>(conn.Table<TermModel>().ToList());

                return Terms;
            }
        }

        public ObservableCollection<CourseModel> GetTermCourses(int termId)
        {
            TermCourses.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var results = conn.Table<CourseModel>().ToList();
                TermCourses = new ObservableCollection<CourseModel>(results.Where(x => x.TermId == termId).ToList());

                return TermCourses;
            }
        }
    }
}
