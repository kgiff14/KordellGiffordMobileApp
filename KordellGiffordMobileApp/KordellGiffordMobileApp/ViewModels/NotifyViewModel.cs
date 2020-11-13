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
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    var results = conn.Table<NotificationModel>().ToList();
                    notify.NotifyOn = results[0].NotifyOn;
                    notify.NotifyDay = results[0].NotifyDay;
                }
            }
            catch
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<NotificationModel>();
                    notify.NotifyOn = true;
                    notify.NotifyDay = 5;
                    conn.Insert(notify);
                }
            }
        }

        public void UpdateNotificationSettings(NotificationModel notification)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Update(notification);
                }
            }
            catch
            {
                return;
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
            try
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
            catch
            {
                return null;
            }
        }
        public List<Tuple<string, DateTime>> GetCourseEndDates()
        {
            try
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
            catch
            {
                return null;
            }
        }
        public List<Tuple<string, DateTime>> GetAssessmentDueDates()
        {
            try
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
            catch
            {
                return null;
            }
        }
    }
}
