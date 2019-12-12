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
    [DesignTimeVisible(false)]
    public partial class NewCoursePage : ContentPage
    {
        private Planner viewModel;

        public NewCoursePage(Planner course)
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

            courseStatusPicker.ItemsSource = new List<string> { 
                Constants.IN_PROGRESS, 
                Constants.COMPLETED, 
                Constants.DROPPED, 
                Constants.PLAN_TO_TAKE
            };
        }


        async void Save_Clicked(object sender, EventArgs e)
        {
            this.viewModel.TermTitle = TermTitlePicker.SelectedItem.ToString();

            // Check that there are no more than 6 courses already for the term selected
            List<Planner> courses = await App.PlannerRepository.GetAllAsync();
            var countCourses = courses.Where(x => x.TermTitle == this.viewModel.TermTitle).Count();
            if (countCourses < 6)
            {
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
                    await App.PlannerRepository.AddNewCourseAsync(this.viewModel);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                var result = await DisplayAlert("Message", "You cannot have more than 6 classes for a term.", null, "OK");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void OnShareButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await ShareText(this.viewModel.CourseNotes);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
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