using KordellGiffordMobileApp.Models;
using Plugin.LocalNotifications;
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


        public void Notifications()
        {
            GetNotificationSettings();
            if (!notify.NotifyOn)
            {
                return;
            }
            int count = 0;
            GetDates();
            for (var i = 0; i < courseStartDates.Count; i++)
            {
                CrossLocalNotifications.Current.Show(courseStartDates[i].Item1, "You course is starting soon!\n" + courseStartDates[i].Item2.ToString("MMM dd, yyyy"), count, courseStartDates[i].Item2.AddDays(-notify.NotifyDay));
                count++;
            }
            for (var i = 0; i < courseEndDates.Count; i++)
            {
                CrossLocalNotifications.Current.Show(courseEndDates[i].Item1, "You course is ending soon!\n" + courseEndDates[i].Item2.ToString("MMM dd, yyyy"), count, courseEndDates[i].Item2.AddDays(-notify.NotifyDay));
                count++;
            }
            for (var i = 0; i < assessmentDueDates.Count; i++)
            {
                CrossLocalNotifications.Current.Show(assessmentDueDates[i].Item1, "You assessment is due soon!\n" + assessmentDueDates[i].Item2.ToString("MMM dd, yyyy"), count, assessmentDueDates[i].Item2.AddDays(-notify.NotifyDay));
                count++;
            }
        }
    }
}
