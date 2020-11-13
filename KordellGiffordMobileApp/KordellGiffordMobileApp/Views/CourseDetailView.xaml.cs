using KordellGiffordMobileApp.Models;
using KordellGiffordMobileApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KordellGiffordMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailView : ContentPage
    {
        public EditCourseDetailViewModel viewModel = new EditCourseDetailViewModel();
        public int id = 0;

        public CourseDetailView(int courseId)
        {
            InitializeComponent();
            GetDetails(courseId);
            id = courseId;
            BindingContext = viewModel;
            SetDetails();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetDetails(id);
            BindingContext = viewModel;
            SetDetails();
        }

        private void GetDetails(int courseId)
        {
            viewModel.GetCourses();
            viewModel.courseList.Clear();
            viewModel.courseList = new ObservableCollection<CourseModel>(viewModel.Courses.Where(x => x.CourseId == courseId).ToList());
        }

        private void SetDetails()
        {
            title.Title = viewModel.courseList[0].CourseName;
            startLabel.Text = viewModel.courseList[0].CourseStartDate.ToString("MMMM dd, yyyy");
            endLabel.Text = viewModel.courseList[0].CourseEndDate.ToString("MMMM dd, yyyy");
            statusPicker.SelectedItem = viewModel.courseList[0].Status;
            instructorNameLabel.Text = viewModel.courseList[0].InstructorName;
            instructorPhoneLabel.Text = viewModel.courseList[0].InstructorPhone;
            instructorEmailLabel.Text = viewModel.courseList[0].InstructorEmail;
            oaNameLabel.Text = viewModel.courseList[0].OAName;
            oaDetailsLabel.Text = viewModel.courseList[0].OADetails;
            oaDueDate.Text = viewModel.courseList[0].OADate.ToString("MMMM dd, yyyy");
            paNameLabel.Text = viewModel.courseList[0].PAName;
            paDetailsLabel.Text = viewModel.courseList[0].PADetails;
            paDueDate.Text = viewModel.courseList[0].PADate.ToString("MMMM dd, yyyy");
            courseNotes.Text = viewModel.courseList[0].Notes;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var results = await DisplayAlert("Save?", "Are you sure you want to save your changes?", "Yes", "No");
            if (results)
            {
                viewModel.course.TermId = viewModel.courseList[0].TermId;
                viewModel.course.CourseId = viewModel.courseList[0].TermId;
                viewModel.course.CourseName = viewModel.courseList[0].CourseName;
                viewModel.course.CourseStartDate = viewModel.courseList[0].CourseStartDate;
                viewModel.course.CourseEndDate = viewModel.courseList[0].CourseEndDate;
                viewModel.course.Status = statusPicker.SelectedItem.ToString();
                viewModel.course.InstructorName = viewModel.courseList[0].InstructorName;
                viewModel.course.InstructorPhone = viewModel.courseList[0].InstructorPhone;
                viewModel.course.InstructorEmail = viewModel.courseList[0].InstructorEmail;
                viewModel.course.OAName = viewModel.courseList[0].OAName;
                viewModel.course.OADetails = oaDetailsLabel.Text;
                viewModel.course.OADate = viewModel.courseList[0].OADate;
                viewModel.course.PAName = viewModel.courseList[0].PAName;
                viewModel.course.PADetails = paDetailsLabel.Text;
                viewModel.course.PADate = viewModel.courseList[0].PADate;
                viewModel.course.Notes = courseNotes.Text;

                viewModel.UpdateCourse(viewModel.course);
                OnAppearing();
            }
        }

        private async void Share_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = viewModel.courseList[0].CourseName + " Notes!",
                Text = courseNotes.Text
            });
        }
    }
}
