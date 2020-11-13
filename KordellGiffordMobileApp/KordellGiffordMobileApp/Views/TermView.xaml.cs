using KordellGiffordMobileApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KordellGiffordMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermView : ContentPage
    {
        public TermViewModel viewModel = new TermViewModel();
        public Models.CourseModel course = new Models.CourseModel();

        public TermView()
        {
            InitializeComponent();

            BindingContext = new TermViewModel();
            SetTermsAndClasses();
        }

        void SetTermsAndClasses()
        {
            viewModel.GetAllTerms();
            PickerTerm.ItemsSource = viewModel.Terms;
            PickerTerm.SelectedIndex = 0;
            var termId = Convert.ToInt32(viewModel.Terms[PickerTerm.SelectedIndex].TermId);
            MyListView.ItemsSource = viewModel.GetTermCourses(termId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new TermViewModel();
            SetTermsAndClasses();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                startLabel.Text = viewModel.Terms[selectedIndex].TermStartDate.ToString("MMMM dd, yyyy");
                endLabel.Text = viewModel.Terms[selectedIndex].TermEndDate.ToString("MMMM dd, yyyy");
                var termId = Convert.ToInt32(viewModel.Terms[selectedIndex].TermId);
                MyListView.ItemsSource = viewModel.GetTermCourses(termId);
            }
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTermView());
        }

        private async void AddCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCourseView());
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var result = await DisplayAlert("Delete Course", "Do you want to delete this course?", "Yes", "No");
            if (result)
            {
                course = (Models.CourseModel)MyListView.SelectedItem;
                viewModel.DeleteTermCourse(course);
                OnAppearing();
            }
        }

        private async void EditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCourseDetailView());
        }
    }
}
