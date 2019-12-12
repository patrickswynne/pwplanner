using System;
using System.Collections.Generic;
using System.Linq;
using WGUPlanner.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }
        //
        // RETURN SEARCH RESULT MATCHE TO ALL STUDENT DATA ROWS
        //
        async void OnSearchButton_Clicked(object sender, EventArgs e)
        {
            // clear the result set
            ResultSet.Children.Clear();

            // get everything
            List<Planner> alldata = await DataAccess.GetAllData();
            // get matching terms 
            List<Planner> terms = await DataAccess.GetAllTerms();

            var searchedTerms = terms.Where(x => x.TermTitle.ToLower().Contains(SearchText.Text.ToLower())).Distinct().ToList();
            if (searchedTerms.Count > 0)
            {
                foreach (var term in searchedTerms)
                {
                    StackLayout termStack = new StackLayout
                    {
                        Padding = new Thickness(5, 5, 5, 5),
                        BackgroundColor = Color.FromHex("#ff4982")
                    };
                    Label termLabel = new Label();
                    termLabel.BackgroundColor = Color.FromHex("#ff4982");
                    termLabel.TextColor = Color.White;


                    termLabel.Text = $"{term.TermTitle} " +
                        $" ({term.TermStartDate.Date} - " +
                        $" {term.TermEndDate.Date})";
                    termLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {
                            await Navigation.PushModalAsync(new NavigationPage(new TermEditPage(term)));
                        })
                    });
                    termStack.Children.Add(termLabel);
                    ResultSet.Children.Add(
                        termStack
                    );
                }
            }

            // matching courses
            List<Planner> courses = await DataAccess.GetAllCourses();

            var searchedCourses = courses.Where(x =>
                x.CourseTitle.ToLower().Contains(SearchText.Text.ToLower())
                || x.CourseNotes.ToLower().Contains(SearchText.Text.ToLower())
                || x.CourseInstructorName.ToLower().Contains(SearchText.Text.ToLower())
                ).Distinct().ToList();
            if (searchedCourses.Count > 0)
            {
                foreach (var course in searchedCourses.Distinct())
                {
                    StackLayout courseStack = new StackLayout
                    {
                        Padding = new Thickness(5, 5, 5, 5),
                        BackgroundColor = Color.FromHex("#8c6591")
                    };
                    Label courseLabel = new Label();
                    courseLabel.BackgroundColor = Color.FromHex("#8c6591");
                    courseLabel.TextColor = Color.White;
                    courseLabel.Text = $"{course.CourseTitle} " +
                        $" ({course.TermStartDate.Date} - " +
                        $" {course.TermEndDate.Date})";
                    courseLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {
                            await Navigation.PushModalAsync(new NavigationPage(new CourseEditPage(course)));
                        })
                    });
                    courseStack.Children.Add(courseLabel);
                    ResultSet.Children.Add(
                        courseStack
                    );
                }
            }

            // get matching assessments
            List<Planner> assessments = await DataAccess.GetAllAssessments();
            var searchedAssessments = assessments.Where(x =>
                x.AssessmentTitle.ToLower().Contains(SearchText.Text.ToLower())
                || x.AssessmentType.ToLower().Contains(SearchText.Text.ToLower())
                ).Distinct().ToList();
            if (searchedAssessments.Count > 0)
            {
                foreach (var assessment in searchedAssessments)
                {
                    StackLayout assessmentStack = new StackLayout
                    {
                        Padding = new Thickness(5, 5, 5, 5),
                        BackgroundColor = Color.FromHex("#fa52c9")
                    };
                    Label assessmentLabel = new Label
                    {
                        Margin = new Thickness(5, 5, 5, 5)
                    };
                    assessmentLabel.BackgroundColor = Color.FromHex("#fa52c9");
                    assessmentLabel.TextColor = Color.White;

                    //assessmentLabel.Margin = 
                    assessmentLabel.Text = $"{assessment.AssessmentTitle} ({assessment.AssessmentType})";
                    assessmentLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                        {
                            await Navigation.PushModalAsync(new NavigationPage(new AssessmentEditPage(assessment)));
                        })
                    });
                    assessmentStack.Children.Add(assessmentLabel);
                    ResultSet.Children.Add(
                        assessmentStack
                    );
                }

                ResultSet.Focus();

            }
        }
    }
}