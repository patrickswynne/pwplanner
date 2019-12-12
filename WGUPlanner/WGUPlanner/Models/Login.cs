using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WGUPlanner.Models
{
    [Table("Users")]
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }

        public string message { get; set; }
        public bool exists { get; set; }

        public static implicit operator string(Users v)
        {
            throw new NotImplementedException();
        }
    }
}
