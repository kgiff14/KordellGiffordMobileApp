using KordellGiffordMobileApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Plugin.LocalNotifications;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KordellGiffordMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursesHomeView : ContentPage
    {
        public CoursesHomeViewModel viewModel = new CoursesHomeViewModel();
        public Models.CourseModel course = new Models.CourseModel();
        public NotifyViewModel notifyViewModel = new NotifyViewModel();

        public CoursesHomeView()
        {
            InitializeComponent();
            HardcodeCheck();
            notifyViewModel.Notifications();
            BindingContext = viewModel;
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
            BindingContext = viewModel;
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

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCourse = (Models.CourseModel)MyListView.SelectedItem;
            int courseId = selectedCourse.CourseId;
            await Navigation.PushAsync(new CourseDetailView(courseId));
        }

        async void Notify_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotificationsView());
        }

        void HardcodeCheck()
        {
            viewModel.GetAllTerms();
            viewModel.GetTermCourses(1);
            notifyViewModel.GetNotificationSettings();
        }
    }
}
