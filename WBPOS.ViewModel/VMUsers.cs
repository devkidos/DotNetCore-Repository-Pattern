using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.ViewModel
{
    public partial class VMUsers
    {
        public System.Guid userId { get; set; }
        public string firstName { get; set; }
        public DateTime? birthDate { get; set; }
        public string sex { get; set; }
        public string mobileNumber { get; set; }
        public string emailId { get; set; }
        public string myRefferalCode { get; set; }
        public string profilePicture { get; set; }
        public string status { get; set; }
        public List<ddlList> statusList { get; set; }

    }
}
