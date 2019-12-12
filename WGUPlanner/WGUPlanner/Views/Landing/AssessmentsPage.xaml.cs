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
    public partial class AssessmentsPage : ContentPage
    {
        public AssessmentsPage()
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
            List<Planner> assessments = await App.PlannerRepository.GetAllAsync();
            var startingAssessments = assessments
                .Where(x => x.AssessmentStartDate >= tomorrow && x.AssessmentStartDate <= nextDay && x.TermNotification == true)
                .Select(y => y).ToList();
            var endingAssessments = assessments
                .Where(x => x.AssessmentEndDate >= tomorrow && x.AssessmentEndDate <= nextDay && x.TermNotification == true)
                .Select(y => y).ToList();
            string message = "";
            foreach (var item in startingAssessments)
            {
                message += $"{item.AssessmentTitle} is starting soon\n";
            }
            foreach (var item in endingAssessments)
            {
                message += $"{item.AssessmentTitle} is due soon\n";
            }
            if (message != "")
            {
                var result = await DisplayAlert("Message", message, "OK", "Dismiss");
                if (result == false)
                {
                    //if the user selected dismiss then clear the notification setting for these.
                    foreach (var planner in startingAssessments)
                    {
                        planner.TermNotification = false;
                        await App.PlannerRepository.UpdateAssessmentAsync(planner);
                    }

                    foreach (var planner in endingAssessments)
                    {
                        planner.TermNotification = false;
                        await App.PlannerRepository.UpdateAssessmentAsync(planner);
                    }

                }
            }
            LoadData();

        }

        public async void LoadData()
        {
            List<Planner> assessments = await App.PlannerRepository.GetAllAsync();
            var distinctAssessments = assessments
                .Where(x => x.AssessmentId != null)
                .GroupBy(p => p.AssessmentId)
                .Select(g => g.First())
                .ToList();

            AssessmentsListView.ItemsSource = distinctAssessments;
        }

        async void OnAssessmentSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var assessment = args.SelectedItem as Planner;
            if (assessment == null)
                return;

            await Navigation.PushModalAsync(new NavigationPage(new AssessmentEditPage(assessment)));

            // Manually deselect item.
            AssessmentsListView.SelectedItem = null;
        }

        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewAssessmentPage(new Planner())));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadData();
        }
    }
}