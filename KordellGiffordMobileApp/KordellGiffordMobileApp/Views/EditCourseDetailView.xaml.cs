using KordellGiffordMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KordellGiffordMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCourseDetailView : ContentPage
    {
        TermViewModel t_viewModel = new TermViewModel();
        EditCourseDetailViewModel viewModel = new EditCourseDetailViewModel();
        public EditCourseDetailView()
        {
            InitializeComponent();

            BindingContext = viewModel;
            t_viewModel.GetAllTerms();
            viewModel.GetCourses();
            PickerTerm.ItemsSource = t_viewModel.Terms;
            coursePicker.ItemsSource = viewModel.Courses;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (ValidateEntries())
            {
                var result = await DisplayAlert("Save Changes", "Are you sure you want to edit this course?", "Yes", "No");
                if (result)
                {
                    var picker = PickerTerm.SelectedIndex;
                    var coursePick = coursePicker.SelectedIndex;
                    if (viewModel.courseList.Where(x => x.TermId == t_viewModel.Terms[picker].TermId).Count() >= 6)
                    {
                        await DisplayAlert("Unable", "There are already 6 classes in that term", "Ok");
                    }
                    else
                    {
                        viewModel.course.TermId = t_viewModel.Terms[picker].TermId;
                        viewModel.course.CourseId = viewModel.Courses[coursePick].CourseId;
                        viewModel.course.CourseName = courseNameEntry.Text;
                        viewModel.course.CourseStartDate = courseStartDate.Date;
                        viewModel.course.CourseEndDate = courseEndDate.Date;
                        viewModel.course.Status = statusPicker.SelectedItem.ToString();
                        viewModel.course.InstructorName = instructorNameEntry.Text;
                        viewModel.course.InstructorPhone = instructorPhoneEntry.Text;
                        viewModel.course.InstructorEmail = instructorEmailEntry.Text;
                        viewModel.course.OAName = oaNameEntry.Text;
                        viewModel.course.OADetails = oaDetailsEditor.Text;
                        viewModel.course.OADate = oaDueDate.Date;
                        viewModel.course.PAName = paNameEntry.Text;
                        viewModel.course.PADetails = paDetailsEditor.Text;
                        viewModel.course.PADate = paDueDate.Date;

                        viewModel.UpdateCourse(viewModel.course);
                        await Navigation.PopAsync();
                    }
                }
            }
            else
            {
                await DisplayAlert("Invalid", "You must fill in all fields and have a valid email address. Start date must be before the end date.", "Ok");
            }
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;


            if (selectedIndex != -1)
            {
                courseNameEntry.Text = viewModel.Courses[selectedIndex].CourseName;
                courseStartDate.Date = viewModel.Courses[selectedIndex].CourseStartDate;
                courseEndDate.Date = viewModel.Courses[selectedIndex].CourseEndDate;
                statusPicker.SelectedItem = viewModel.Courses[selectedIndex].Status;
                instructorNameEntry.Text = viewModel.Courses[selectedIndex].InstructorName;
                instructorPhoneEntry.Text = viewModel.Courses[selectedIndex].InstructorPhone;
                instructorEmailEntry.Text = viewModel.Courses[selectedIndex].InstructorEmail;
                oaNameEntry.Text = viewModel.Courses[selectedIndex].OAName;
                oaDetailsEditor.Text = viewModel.Courses[selectedIndex].OADetails;
                oaDueDate.Date = viewModel.Courses[selectedIndex].OADate;
                paNameEntry.Text = viewModel.Courses[selectedIndex].PAName;
                paDetailsEditor.Text = viewModel.Courses[selectedIndex].PADetails;
                paDueDate.Date = viewModel.Courses[selectedIndex].PADate;
                PickerTerm.SelectedItem = t_viewModel.Terms.Where(x => x.TermId == viewModel.Courses[selectedIndex].TermId).Select(x => x.TermName);
            }
        }

        private bool ValidateEntries()
        {
            if (PickerTerm.SelectedIndex == -1 || string.IsNullOrEmpty(courseNameEntry.Text)
                || statusPicker.SelectedIndex == -1 || string.IsNullOrEmpty(instructorNameEntry.Text)
                || string.IsNullOrEmpty(instructorNameEntry.Text) || string.IsNullOrEmpty(instructorPhoneEntry.Text)
                || string.IsNullOrEmpty(instructorEmailEntry.Text) || string.IsNullOrEmpty(oaNameEntry.Text)
                || string.IsNullOrEmpty(paNameEntry.Text) || !Regex.IsMatch(instructorEmailEntry.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                || string.IsNullOrEmpty(oaDetailsEditor.Text) || string.IsNullOrEmpty(paDetailsEditor.Text)
                || courseStartDate.Date > courseEndDate.Date)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}