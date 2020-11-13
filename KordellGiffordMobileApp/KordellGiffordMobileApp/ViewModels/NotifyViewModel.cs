using KordellGiffordMobileApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KordellGiffordMobileApp.ViewModels
{
    public class NotifyViewModel : BaseViewModel
    {
        public NotificationModel notify = new NotificationModel();
        public List<Tuple<string, DateTime>> courseStartDates = new List<Tuple<string, DateTime>>();
        public List<Tuple<string, DateTime>> courseEndDates = new List<Tuple<string, DateTime>>();
        public List<Tuple<string, DateTime>> assessmentDueDates = new List<Tuple<string, DateTime>>();

        public void GetNotificationSettings()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var results = conn.Table<NotificationModel>().ToList();
                notify.NotifyOn = results[0].NotifyOn;
                notify.NotifyDay = results[0].NotifyDay;
            }
        }

        public void UpdateNotificationSettings(NotificationModel notification)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.Update(notification);
            }
        }

        public void GetDates()
        {
            GetAssessmentDueDates();
            GetCourseEndDates();
            GetCourseStartDates();
        }

        public List<Tuple<string, DateTime>> GetCourseStartDates()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var results = conn.Table<CourseModel>().ToList();
                for (var i = 0; i < results.Count; i++)
                {
                    courseStartDates.Add(new Tuple<string, DateTime>(results[i].CourseName, results[i].CourseStartDate));
                }
                return courseStartDates;
            }
        }
        public List<Tuple<string, DateTime>> GetCourseEndDates()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var results = conn.Table<CourseModel>().ToList();
                for (var i = 0; i < results.Count; i++)
                {
                    courseEndDates.Add(new Tuple<string, DateTime>(results[i].CourseName, results[i].CourseEndDate));
                }
                return courseEndDates;
            }
        }
        public List<Tuple<string, DateTime>> GetAssessmentDueDates()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var results = conn.Table<CourseModel>().ToList();
                for (var i = 0; i < results.Count; i++)
                {
                    assessmentDueDates.Add(new Tuple<string, DateTime>(results[i].OAName, results[i].OADate));
                    assessmentDueDates.Add(new Tuple<string, DateTime>(results[i].PAName, results[i].PADate));
                }
                return assessmentDueDates;
            }
        }
    }
}
