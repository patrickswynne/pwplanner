using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGUPlanner.Models;
using WGUPlanner.Util;

namespace WGUPlanner
{
    public static class DataAccess
    {
        public static async Task<List<Planner>> GetAllData()
        {
            return await App.PlannerRepository.GetAllAsync();
        }
        public static async Task<List<Planner>> GetAllTerms()
        {
            List<Planner> terms = await App.PlannerRepository.GetAllAsync();
            List<Planner> distinctTerms = terms
                .Where(x => x.TermTitle != null)
                .GroupBy(p => p.TermTitle)
                .Select(g => g.First())
                .ToList();
            return distinctTerms;
        }
        public static async Task<List<Planner>> GetAllCourses()
        {
            List<Planner> courses = await App.PlannerRepository.GetAllAsync();
            var distinctCourses = courses
                .Where(x => x.CourseTitle != null)
                .GroupBy(p => p.CourseTitle)
                .Select(g => g.First())
                .ToList();
            return distinctCourses;
        }
        public static async Task<List<Planner>> GetAllAssessments()
        {
            List<Planner> assessments = await App.PlannerRepository.GetAllAsync();
            var distinctAssessments = assessments
                .Where(x => x.AssessmentId != null)
                .GroupBy(p => p.AssessmentId)
                .Select(g => g.First())
                .ToList();
            return distinctAssessments;
        }
        public static async Task<List<Report>> GetReportData()
        {
            List<Report> reportList = new List<Report>();


            // 1. populate the scrollview with the combined results;
            var termsResult = (from r in await App.PlannerRepository.GetAllAsync()
                               where r.TermEndDate < DateTime.Now.Date
                               group r.TermTitle by r.TermTitle into t
                               select t).ToList();
            reportList.Add(ReportUtil.getReport(termsResult.Count, "Terms Completed"));

            // 2. Classes completed :  # of classes completed
            var courseResult = (from c in await App.PlannerRepository.GetAllAsync()
                                where c.CourseEndDate < DateTime.Now.Date
                                group c.CourseTitle by c.CourseTitle into a
                                select a).ToList();
            reportList.Add(ReportUtil.getReport(courseResult.Count, "Courses Completed"));

            // 3. Objectives completed: # of objectives completed
            var objectiveResult = (from c in await App.PlannerRepository.GetAllAsync()
                                   where c.AssessmentEndDate < DateTime.Now.Date
                                   group c.AssessmentTitle by c.AssessmentTitle into a
                                   select a).ToList();
            reportList.Add(ReportUtil.getReport(objectiveResult.Count, "Assessments Completed"));

            // 4. # of active classes
            var activeResult = (from c in await App.PlannerRepository.GetAllAsync()
                                where c.CourseEndDate > DateTime.Now.Date
                                group c.CourseTitle by c.CourseTitle into a
                                select a).ToList();
            reportList.Add(ReportUtil.getReport(activeResult.Count, "Active Classes"));

            return reportList;
        }
    }
}
