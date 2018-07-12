using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEW_CAM.Models
{
    public class UserDetails
    {
        public string department { get; set; }

        public string samaccountname { get; set; }
        public string mail { get; set; }
        public string displayname { get; set; }
        public string title { get; set; }
        public List<string> directreports { get; set; }


        public string mobile { get; set; }
    }
}