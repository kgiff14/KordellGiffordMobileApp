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
    public partial class EditTermView : ContentPage
    {
        TermViewModel viewModel = new TermViewModel();
        TermModel term = new TermModel();
        TermView termView = new TermView();
        public EditTermView()
        {
            InitializeComponent();

            BindingContext = new TermViewModel();
            viewModel.GetAllTerms();
            PickerTerm.ItemsSource = viewModel.Terms;
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                termNameEntry.Text = viewModel.Terms[selectedIndex].TermName;
                termStartDate.Date = viewModel.Terms[selectedIndex].TermStartDate;
                termEndDate.Date = viewModel.Terms[selectedIndex].TermEndDate;
            }
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            if (ValidateEntries())
            {
                var result = await DisplayAlert("Update", "Are you sure you want to update this term?", "Yes", "No");
                if (result)
                {
                    var picker = PickerTerm.SelectedIndex;

                    term.TermId = viewModel.Terms[picker].TermId;
                    term.TermName = termNameEntry.Text;
                    term.TermStartDate = termStartDate.Date;
                    term.TermEndDate = termEndDate.Date;

                    viewModel.UpdateTerm(term);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Invalid", "All entries must be filled and start date must be before end date.", "ok");
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Delete", "Are you sure you want to delete this term?", "Yes", "No");
            if (result)
            {
                var picker = PickerTerm.SelectedIndex;

                term.TermId = viewModel.Terms[picker].TermId;
                term.TermName = termNameEntry.Text;
                term.TermStartDate = termStartDate.Date;
                term.TermEndDate = termEndDate.Date;

                viewModel.DeleteTerm(term);
                await Navigation.PopAsync();
            }
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            if (ValidateEntries())
            {
                var result = await DisplayAlert("Add", "Are you sure you want to add this term?", "Yes", "No");
                if (result)
                {
                    term.TermName = termNameEntry.Text;
                    term.TermStartDate = termStartDate.Date;
                    term.TermEndDate = termEndDate.Date;

                    viewModel.AddTerm(term);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Invalid", "All entries must be filled and start date must be before end date.", "ok");
            }

        }

        bool ValidateEntries()
        {
            if (string.IsNullOrEmpty(termNameEntry.Text) || termStartDate.Date > termEndDate.Date)
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