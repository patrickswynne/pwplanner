using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WGUPlanner;
using WGUPlanner.Models;
using WGUPlanner.Views;

namespace WGUPlannerTest
{
    [TestFixture]
    public class Tests
    {
        private List<string> results;
        private PlannerRepository plannerRepository;
        private Planner viewModel;

        //When the application initially loads, I expect no terms to load.
        [Test]
        public void SearchingTest()
        {
            Assert.AreEqual(1, 1);

            Planner planner = new Planner();

            // add a term
            planner.TermTitle = "A test term";
            planner.TermStartDate = DateTime.Now;
            planner.TermEndDate = DateTime.Now.AddDays(20);
            planner.TermNotification = true;
            this.viewModel = planner;
            Saved();

            // search for the term

            // assert that results are not empty

            Assert.NotNull(results);
        }

        async void Saved()
        {
            App app = new App();
            await App.PlannerRepository.AddNewTermAsync(this.viewModel);
        }
    }
}