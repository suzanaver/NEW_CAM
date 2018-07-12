using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NEW_CAM.Models
{
    public class CamContactList
    {
        public string contact1 { get; set; }
        [RegularExpression(@"[^a-zA-Z]+$", ErrorMessage = "Entered phone format is not valid.")]
        public string txtphonecellular { get; set; }
        [RegularExpression(@"[^a-zA-Z]+$", ErrorMessage = "Entered phone format is not valid.")]
        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string txtphonecellular2 { get; set; }
        public string remarks { get; set; }
    }
}