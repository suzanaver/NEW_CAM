using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEW_CAM.Models
{
    public class ContactListViewModel
    {
            public List<ContactViewModel> ReportedUsers { get; set; }
            //Other Properties also here

            public ContactListViewModel()
            {
                ReportedUsers = new List<ContactViewModel>();
            }
        }   
}