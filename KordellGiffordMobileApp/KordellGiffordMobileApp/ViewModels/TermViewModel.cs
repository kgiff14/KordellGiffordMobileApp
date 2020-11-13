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
            try
            {
                Terms.Clear();
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<TermModel>();
                    Terms = new ObservableCollection<TermModel>(conn.Table<TermModel>().ToList());
                 
                    return Terms;
                }
            }
            catch { return null; }
        }

        public void AddTerm(TermModel term)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Insert(term);
                }
            }
            catch { }
        }

        public void UpdateTerm(TermModel term)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Update(term);
                }
            }
            catch { }
        }

        public void DeleteTerm(TermModel term)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Delete(term);
                }
            }
            catch { }
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
            catch { return null; }
        }

        public void DeleteTermCourse(CourseModel course)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Delete(course);
                }
            }
            catch { }
        }
    }
}
