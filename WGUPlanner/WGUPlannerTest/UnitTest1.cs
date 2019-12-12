using NUnit.Framework;
using SQLite;
using System;
using System.Threading.Tasks;
using WGUPlanner;
using WGUPlanner.Models;
using Xunit;
using WGUPlanner.Implementation;
using System.Collections.Generic;

namespace WGUPlannerTest
{
    [TestFixture]
    public class Tests
    {
        private const string username = "test";
        private const string password = "test";
        BusinessImplementation bi = new BusinessImplementation();

        private Tests()
        {
            // do not instantiate
            throw new Exception("illegal instantiation") as ResultStateException;
        }



        // when I provide missing credentials they are invalid
        [Test]
        public async System.Threading.Tasks.Task UserIsInvalid()
        {
            Users user = new Users();
            user.Email = username;
            user.Password = null;
            NUnit.Framework.Assert.AreEqual(bi.validUser(user), false);
        }

        // when I provide complete credentials they are valid
        [Test]
        public async System.Threading.Tasks.Task UserIsValid()
        {
            Users user = new Users();
            user.Email = username;
            user.Password = password;
            NUnit.Framework.Assert.AreEqual(bi.validUser(user), true);
        }

        // when I search results should not be null
        [Test]
        public void SearchIsNotNull()
        {
            NUnit.Framework.Assert.IsNotNull(DataAccess.GetAllData());
        }
        // when I request a report it shoud not be null
        [Test]
        public void ReportIsNotNull()
        {
            NUnit.Framework.Assert.IsNotNull(DataAccess.GetReportData());
        }
    }
}