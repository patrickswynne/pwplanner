using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WGUPlanner.Models;
using Xamarin.Forms;

namespace WGUPlanner.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TermsPage : ContentPage
    {

        public TermsPage()
        {
            InitializeComponent();

            LoadData();

            Notifications();
        }
        public async void Notifications()
        {
            var tomorrow = DateTime.Now.AddDays(1).Date;
            var nextDay = tomorrow.AddDays(1).Date;
            //check if any terms are ending in the next day, or starting in the next day
            List<Planner> terms = await App.PlannerRepository.GetAllAsync();
            var startingTerms = terms
                .Where(x => x.TermStartDate >= tomorrow && x.TermStartDate <= nextDay && x.TermNotification == true)
                .Select(y => y).ToList();
            var endingTerms = terms
                .Where(x => x.TermEndDate >= tomorrow && x.TermEndDate <= nextDay && x.TermNotification == true)
                .Select(y => y).ToList();
            string message = "";
            foreach (var item in startingTerms)
            {
                message += $"{item.TermTitle} is starting soon\n";
            }
            foreach (var item in startingTerms)
            {
                message += $"{item.TermTitle} is ending\n";
            }
            if (message != "")
            {
                var result = await DisplayAlert("Message", message, "OK", "Dismiss");
                if (result == false)
                {
                    //if the user selected dismiss then clear the notification setting for these.
                    foreach (var planner in startingTerms)
                    {
                        planner.TermNotification = false;
                        await App.PlannerRepository.UpdateTermAsync(planner);
                    }

                    foreach (var planner in endingTerms)
                    {
                        planner.TermNotification = false;
                        await App.PlannerRepository.UpdateTermAsync(planner);
                    }

                    //await Navigation.PopModalAsync();

                }
            }
            LoadData();

        }
        public async void LoadData()
        {
            List<Planner> terms = await App.PlannerRepository.GetAllAsync();
            var distinctTerms = terms
                .Where(x => x.TermTitle != null)
                .GroupBy(p => p.TermTitle)
                .Select(g => g.First())
                .ToList();
            TermsListView.ItemsSource = distinctTerms;
        }
        async void OnTermSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var term = args.SelectedItem as Planner;
            if (term == null)
                return;

            await Navigation.PushModalAsync(new NavigationPage(new TermEditPage(term)));

            // Manually deselect item.
            TermsListView.SelectedItem = null;
        }
        async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewTermPage(new Planner())));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadData();

            Notifications();
        }
    }
}