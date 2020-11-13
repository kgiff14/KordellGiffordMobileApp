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
    public partial class AddCourseView : ContentPage
    {
        AddCourseViewModel viewModel = new AddCourseViewModel();
        TermViewModel t_viewModel = new TermViewModel();
        public AddCourseView()
        {
            InitializeComponent();
            BindingContext = viewModel;
            t_viewModel.GetAllTerms();
            PickerTerm.ItemsSource = t_viewModel.Terms;
            viewModel.GetCourses();
        }

        public async void Add_Clicked(object sender, EventArgs e)
        {
            if (ValidateEntries())
            {
                var result = await DisplayAlert("Add Course", "Are you sure you want to add this course?", "Yes", "No");
                if (result)
                {
                    var picker = PickerTerm.SelectedIndex;
                    if (viewModel.Courses.Where(x => x.TermId == t_viewModel.Terms[picker].TermId).Count() >= 6)
                    {
                        await DisplayAlert("Unable", "There are already 6 classes in that term", "Ok");
                    }
                    else
                    {
                        viewModel.course.TermId = t_viewModel.Terms[picker].TermId;
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

                        viewModel.AddCourse(viewModel.course);
                        await Navigation.PopAsync();
                    }
                }
            }
            else
            {
                await DisplayAlert("Invalid", "You must fill in all fields and have a valid email address. Start date must be before end dates.", "Ok");
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