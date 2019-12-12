using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WGUPlanner.Models;


namespace WGUPlanner
{
    public class PlannerRepository
    {
        // A PRIVATE CONNECTION STRING FOR THIS REPOSITORY
        private SQLiteAsyncConnection conn;
        //INJECT THE DBPATH FROM THE 
        public PlannerRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Users>().Wait();
        }
        //
        // ADD A NEW TERM
        //
        public async Task AddNewTermAsync(Planner planner)
        {
            int result = 0;
            try
            {
                result = await conn.InsertAsync(planner);
                Console.WriteLine(string.Format("{0} record(s) added [Name: {1})", result, planner.TermTitle));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to add {0}. Error: {1}", planner.TermTitle, ex.Message));
            }
        }
        //
        // UPDATE A NEW TERM
        //
        public async Task UpdateTermAsync(Planner planner)
        {
            foreach(Planner term in await GetAllAsync())
            {
                if(term.TermTitle == planner.TermTitle)
                {
                    Planner temp = new Planner();


                    temp.Id = term.Id;
                    temp.TermTitle = planner.TermTitle;
                    temp.TermStartDate = planner.TermStartDate.Date;
                    temp.TermEndDate = planner.TermEndDate.Date;
                    await conn.UpdateAsync(temp);
                }
            }
        }
        //
        // GET ALL STUDENT DATA
        //
        public async Task<List<Planner>> GetAllAsync()
        {
            try
            {
                return await conn.Table<Planner>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to retrieve data. {0}", ex.Message));
            }

            return new List<Planner>();
        }
        //
        // ADD A NEW COURSE
        //
        public async Task AddNewCourseAsync(Planner planner)
        {

            int result = 0;
            try
            {
                result = await conn.InsertAsync(planner);

                Console.WriteLine(string.Format("{0} record(s) added [Name: {1})", result, planner.CourseTitle));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to add {0}. Error: {1}", planner.CourseTitle, ex.Message));
            }
        }
        //
        // UPDATE A NEW COURSE
        //
        public async Task UpdateCourseAsync(Planner planner)
        {
            await conn.UpdateAsync(planner);
        }
        //
        // ADD A NEW ASSESSMENT
        //
        public async Task AddNewAssessmentAsync(Planner planner)
        {

            int result = 0;
            try
            {
                result = await conn.InsertAsync(planner);

                Console.WriteLine(string.Format("{0} record(s) added [Name: {1})", result, planner.AssessmentTitle));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        //
        // ADD AN ASSESSMENT
        //
        public async Task UpdateAssessmentAsync(Planner planner)
        {
            await conn.UpdateAsync(planner);
        }
        //
        // DELETE A TERM
        //
        public async Task<bool> DeleteTermAsync(string termTitle)
        {
            await conn.Table<Planner>().DeleteAsync(x => x.TermTitle == termTitle);
            return await Task.FromResult(true);
        }
        //
        // ADD A COURSE
        //
        public async Task<bool> DeleteCourseAsync(string courseTitle)
        {
            await conn.Table<Planner>().DeleteAsync(x => x.CourseTitle == courseTitle);
            return await Task.FromResult(true);
        }
        //
        // ADD AN ASSESSMENT
        //
        public async Task<bool> DeleteAssessmentAsync(string assessmentTitle)
        {
            await conn.Table<Planner>().DeleteAsync(x => x.AssessmentTitle == assessmentTitle);
            return await Task.FromResult(true);
        }
    }
}
