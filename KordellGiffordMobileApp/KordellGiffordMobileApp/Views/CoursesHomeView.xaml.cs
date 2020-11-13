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
            Notifications();
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

        void Notifications()
        {
            notifyViewModel.GetNotificationSettings();
            if (!notifyViewModel.notify.NotifyOn)
            {
                return;
            }
            int count = 0;
            notifyViewModel.GetDates();
            for (var i = 0; i < notifyViewModel.courseStartDates.Count; i++)
            {
                CrossLocalNotifications.Current.Show(notifyViewModel.courseStartDates[i].Item1, "You course is starting soon!\n" + notifyViewModel.courseStartDates[i].Item2.ToString("MMM dd, yyyy"), count, notifyViewModel.courseStartDates[i].Item2.AddDays(-notifyViewModel.notify.NotifyDay));
                count++;
            }
            for (var i = 0; i < notifyViewModel.courseEndDates.Count; i++)
            {
                CrossLocalNotifications.Current.Show(notifyViewModel.courseEndDates[i].Item1, "You course is ending soon!\n" + notifyViewModel.courseEndDates[i].Item2.ToString("MMM dd, yyyy"), count, notifyViewModel.courseEndDates[i].Item2.AddDays(-notifyViewModel.notify.NotifyDay));
                count++;
            }
            for (var i = 0; i < notifyViewModel.assessmentDueDates.Count; i++)
            {
                CrossLocalNotifications.Current.Show(notifyViewModel.assessmentDueDates[i].Item1, "You assessment is due soon!\n" + notifyViewModel.assessmentDueDates[i].Item2.ToString("MMM dd, yyyy"), count, notifyViewModel.assessmentDueDates[i].Item2.AddDays(-notifyViewModel.notify.NotifyDay));
                count++;
            }
        }

        void HardcodeCheck()
        {
            viewModel.GetAllTerms();
            viewModel.GetTermCourses(0);
            notifyViewModel.GetNotificationSettings();
        }
    }
}
