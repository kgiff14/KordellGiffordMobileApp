using KordellGiffordMobileApp.Models;
using KordellGiffordMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KordellGiffordMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsView : ContentPage
    {
        public NotifyViewModel notification = new NotifyViewModel();
        public NotificationModel model = new NotificationModel();
        public NotificationsView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            notification.GetNotificationSettings();
            notificationCheck.IsChecked = notification.notify.NotifyOn;
            notificationPicker.SelectedItem = notification.notify.NotifyDay.ToString();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (notificationPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Invalid", "Select a time frame", "Ok");
            }
            else
            {
                var results = await DisplayAlert("Save", "Are you sure you want to update your notification settings?", "Yes", "No");
                if (results)
                {
                    model.NotifyId = notification.notify.NotifyId;
                    model.NotifyOn = notificationCheck.IsChecked;
                    model.NotifyDay = Convert.ToInt32(notificationPicker.SelectedItem);
                    notification.UpdateNotificationSettings(model);
                }
            }
            OnAppearing();
        }
    }
}