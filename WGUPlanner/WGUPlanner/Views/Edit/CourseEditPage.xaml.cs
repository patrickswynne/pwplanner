using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WGUPlanner.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WGUPlanner.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CourseEditPage : ContentPage
    {
        private Planner viewModel;
        public CourseEditPage(Planner course)
        {
            InitializeComponent();

            BindingContext = this.viewModel = course;

            LoadTermList();
        }

        async void LoadTermList()
        {
            List<Planner> terms = await App.PlannerRepository.GetAllAsync();
            var distinctTerms = terms.Where(x => x.TermTitle != null).Select(y => y.TermTitle).Distinct().ToList();
            TermTitlePicker.ItemsSource = distinctTerms;
            CourseStatusPicker.ItemsSource = new List<string> {
                Constants.IN_PROGRESS,
                Constants.COMPLETED,
                Constants.DROPPED,
                Constants.PLAN_TO_TAKE
            };
            TermTitlePicker.SelectedItem = this.viewModel.TermTitle;
            CourseStatusPicker.SelectedItem = this.viewModel.CourseStatus;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            this.viewModel.TermTitle = TermTitlePicker.SelectedItem.ToString();
            this.viewModel.CourseStatus = CourseStatusPicker.SelectedItem.ToString();

            if (
                this.viewModel.CourseInstructorEmail == null ||
                this.viewModel.CourseInstructorName == null ||
                this.viewModel.CourseInstructorPhone == null ||
                this.viewModel.CourseTitle == null ||
                this.viewModel.TermTitle == null ||
                this.viewModel.CourseInstructorEmail == "" ||
                this.viewModel.CourseInstructorName == "" ||
                this.viewModel.CourseInstructorPhone == "" ||
                this.viewModel.CourseTitle == "" ||
                this.viewModel.TermTitle == ""
                )
            {
                var result = await DisplayAlert("Message", Constants.MISSING_INFORMATION, null, "OK");
            }
            else
            {
                await App.PlannerRepository.UpdateCourseAsync(this.viewModel);
                await Navigation.PopModalAsync();
            }
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await App.PlannerRepository.DeleteCourseAsync(this.viewModel.CourseTitle);
            await Navigation.PopModalAsync();
        }

        async void OnShareButton_Clicked(object sender, EventArgs e)
        {
            await ShareText(this.viewModel.CourseNotes);
        }

        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }
    }
}