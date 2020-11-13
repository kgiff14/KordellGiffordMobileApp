using KordellGiffordMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Linq;
using System.Collections.ObjectModel;

namespace KordellGiffordMobileApp.ViewModels
{
    public class TermViewModel : BaseViewModel
    {
        public ObservableCollection<TermModel> Terms = new ObservableCollection<TermModel>();
        public ObservableCollection<CourseModel> TermCourses = new ObservableCollection<CourseModel>();
        public CourseModel course = new CourseModel();



        public ObservableCollection<TermModel> GetAllTerms()
        {
            Terms.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<TermModel>();
                Terms = new ObservableCollection<TermModel>(conn.Table<TermModel>().ToList());
                 
                return Terms;
            }
        }

        public void AddTerm(TermModel term)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Insert(term);
            }
        }

        public void UpdateTerm(TermModel term)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Update(term);
            }
        }

        public void DeleteTerm(TermModel term)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Delete(term);
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

        public void DeleteTermCourse(CourseModel course)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Delete(course);
            }
        }
    }
}
