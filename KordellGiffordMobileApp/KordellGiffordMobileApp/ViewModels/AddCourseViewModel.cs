﻿using KordellGiffordMobileApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KordellGiffordMobileApp.ViewModels
{
    public class AddCourseViewModel : BaseViewModel
    {
        public CourseModel course = new CourseModel();
        public ObservableCollection<CourseModel> Courses = new ObservableCollection<CourseModel>();

        public ObservableCollection<CourseModel> GetCourses()
        {
            try
            {
                Courses.Clear();
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    Courses = new ObservableCollection<CourseModel>(conn.Table<CourseModel>().ToList());
                }

                return Courses;
            }
            catch { return null; }
        }

        public void AddCourse(CourseModel course)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<CourseModel>();
                    conn.Insert(course);
                }
            }
            catch { return; }
        }
    }
}
