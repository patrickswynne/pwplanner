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
    public partial class CoursesPage : ContentPage
    {
        public CoursesPage()
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
            List<Planner> courses = await App.PlannerRepository.GetAllAsync();
            var startingCourses = courses
                .Where(x => x.CourseStartDate >= tomorrow && x.CourseStartDate <= nextDay && x.TermNotification == true)
                .Select(y => y).ToList();
            var endingTerms = courses
                .Where(x => x.CourseEndDate >= tomorrow && x.CourseEndDate <= nextDay && x.TermNotification == true)
                .Select(y => y).ToList();
            string message = "";
            foreach (var item in startingCourses)
            {
                message += $"{item.CourseTitle} is starting \n";
            }
            foreach (var item in endingTerms)
            {
                message += $"{item.CourseTitle} is ending\n";
            }
            if (message != "")
            {
                var result = await DisplayAlert("Message", message, "OK", "Dismiss");
                if (result == false)
                {
                    //if the user selected dismiss then clear the notification setting for these.
                    foreach (var planner in startingCourses)
                    {
                        planner.TermNotification = false;
                        await App.PlannerRepository.UpdateCourseAsync(planner);
                    }

                    foreach (var planner in endingTerms)
                    {
                        planner.TermNotification = false;
                        await App.PlannerRepository.UpdateCourseAsync(planner);
                    }

                }
            }
            LoadData();

        }
        public async void LoadData()
        {
            List<Planner> courses = await App.PlannerRepository.GetAllAsync();
            var distinctCourses = courses
                .Where(x => x.CourseTitle != null)
                .GroupBy(p => p.CourseTitle)
                .Select(g => g.First())
                .ToList();
            CoursesListView.ItemsSource = distinctCourses;
        }
        async void OnCourseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var course = args.SelectedItem as Planner;
            if (course == null)
                return;

            await Navigation.PushModalAsync(new NavigationPage(new CourseEditPage(course)));

            // Manually deselect item.
            CoursesListView.SelectedItem = null;
        }
        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCoursePage(new Planner())));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadData();
        }
    }
}