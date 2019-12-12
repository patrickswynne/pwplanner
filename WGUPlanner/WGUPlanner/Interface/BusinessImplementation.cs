using System;
using System.Collections.Generic;
using System.Text;
using WGUPlanner.Models;

namespace WGUPlanner.Implementation
{
    public class BusinessImplementation : Interface.BusinessInterface
    {
        public bool validUser(Users user)
        {
            if (!String.IsNullOrEmpty(user.Email) && !String.IsNullOrEmpty(user.Password))
            {
                return true;
            }
            return false;
        }
    }
}
