using System;
using System.Collections.Generic;
using System.Text;
using WGUPlanner.Models;

namespace WGUPlanner.FakeData
{
    public static class CreateFakeStudent
    {
        public async static void CreateStudentData()
        {
            // load a fake user
            Users initUser = new Users();
            initUser.Email = "test";
            initUser.Password = "test ";
            await App.LoginRepository.AddNewUserAsync(initUser);

            // course 2 
            Planner planner = new Planner();
            planner.TermTitle = "Spring 2099";
            planner.CourseTitle = "Geometry";
            planner.CourseStatus = "In Progress";
            planner.CourseStartDate = DateTime.Parse("10/01/2019");
            planner.CourseEndDate = DateTime.Parse("09/01/2020");
            planner.CourseNotification = true;
            planner.CourseNotes = "nothing yet";
            planner.CourseInstructorName = "Patrick Wynne";
            planner.CourseInstructorEmail = "pwynne1@wgu.edu";
            planner.CourseInstructorPhone = "972-748-3218";
            await App.PlannerRepository.AddNewCourseAsync(planner);

            // Term
            planner.TermTitle = "Spring 2099";
            planner.TermNotification = true;
            planner.TermStartDate = DateTime.Parse("03/01/2019");
            planner.TermEndDate = DateTime.Parse("09/01/2019");
            // Course
            //planner = new Planner();
            planner.TermTitle = "Spring 2099";
            planner.CourseTitle = "Programming 101";
            planner.CourseStatus = "Plan To Take";
            planner.CourseStartDate = DateTime.Parse("03/01/2019");
            planner.CourseEndDate = DateTime.Parse("09/01/2019");
            planner.CourseNotification = true;
            planner.CourseNotes = "nothing yet";
            planner.CourseInstructorName = "Patrick Wynne";
            planner.CourseInstructorEmail = "pwynne1@wgu.edu";
            planner.CourseInstructorPhone = "972-748-3218";
            // Assessments
            //planner = new Planner();            
            planner.CourseTitle = "Programming 101";
            planner.AssessmentTitle = "Programming Performance A.";
            planner.AssessmentNotification = true;
            planner.AssessmentType = "performance";
            planner.AssessmentId = "Programming 101performance";
            planner.AssessmentStartDate = DateTime.Parse("03/01/2019");
            planner.AssessmentEndDate = DateTime.Parse("09/01/2019");
            await App.PlannerRepository.AddNewAssessmentAsync(planner);


            // Term
            planner = new Planner();
            planner.TermTitle = "Spring 2099";
            planner.TermNotification = true;
            planner.TermStartDate = DateTime.Parse("03/01/2019");
            planner.TermEndDate = DateTime.Parse("09/01/2019");
            // Course
            //planner = new Planner();
            planner.TermTitle = "Spring 2099";
            planner.CourseTitle = "Programming 101";
            planner.CourseStatus = "Plan To Take";
            planner.CourseStartDate = DateTime.Parse("03/01/2019");
            planner.CourseEndDate = DateTime.Parse("09/01/2019");
            planner.CourseNotification = true;
            planner.CourseNotes = "nothing yet";
            planner.CourseInstructorName = "Patrick Wynne";
            planner.CourseInstructorEmail = "pwynne1@wgu.edu";
            planner.CourseInstructorPhone = "972-748-3218";
            // Assessments
            planner.CourseTitle = "Programming 101";
            planner.AssessmentTitle = "Programming Objective A.";
            planner.AssessmentNotification = true;
            planner.AssessmentType = "objective";
            planner.AssessmentId = "Programming 101objective";
            planner.AssessmentStartDate = DateTime.Parse("03/01/2019");
            planner.AssessmentEndDate = DateTime.Parse("09/01/2019");
            await App.PlannerRepository.AddNewAssessmentAsync(planner);
        }
    }
}
