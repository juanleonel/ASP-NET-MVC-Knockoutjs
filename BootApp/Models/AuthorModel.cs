using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootApp.Models
{
    public class AuthorModel
    {
        
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

        public DateTime Created_at { get; set; }
    }
}