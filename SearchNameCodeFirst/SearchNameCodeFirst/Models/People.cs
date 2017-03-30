using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchNameCodeFirst.Models
{
    public class People
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public int Age { get; set; }
        public string Interest { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
    }
}