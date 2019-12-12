using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WGUPlanner.Models;
using Xamarin.Forms;

namespace WGUPlanner.Views
{
    [DesignTimeVisible(false)]
    public partial class NewAssessmentPage : ContentPage
    {
        private Planner viewModel;

        public NewAssessmentPage(Planner assessment)
        {
            InitializeComponent();

            BindingContext = this.viewModel = assessment;

            LoadCourseList();
        }
        async void LoadCourseList()
        {
            List<Planner> courses = await App.PlannerRepository.GetAllAsync();
            var distinctCourses = courses.Where(x => x.CourseTitle != null).Select(y => y.CourseTitle).Distinct().ToList();
            CourseTitlePicker.ItemsSource = distinctCourses;

            CourseTitlePicker.SelectedItem = this.viewModel.CourseTitle;
            AssessmentType.SelectedItem = this.viewModel.AssessmentType;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            this.viewModel.AssessmentId = CourseTitlePicker.SelectedItem.ToString() + AssessmentType.SelectedItem;

            //check that this assessment type + course does not exist already
            List<Planner> assessments = await App.PlannerRepository.GetAllAsync();
            var countAssessments = assessments.Where(x => x.AssessmentId == this.viewModel.AssessmentId).Count();

            if (countAssessments == 0)
            {
                this.viewModel.AssessmentType = AssessmentType.SelectedItem.ToString();
                this.viewModel.CourseTitle = CourseTitlePicker.SelectedItem.ToString();
                await App.PlannerRepository.AddNewAssessmentAsync(this.viewModel);
                await Navigation.PopModalAsync();
            }
            else
            {
                var result = await DisplayAlert("Message", "Only 1 objective and 1 performance assessment may be created for each course.", null, "OK");
            }

        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}