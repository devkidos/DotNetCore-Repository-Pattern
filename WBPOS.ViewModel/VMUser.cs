using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.ViewModel
{

    public partial class VMUser
    {
        public System.Guid userId { get; set; }
        public Nullable<System.Guid> organizationId { get; set; }
        public string usertype { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salutation { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string sex { get; set; }
        public string socialSecurityNnumber { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public Nullable<System.Guid> cityId { get; set; }
        public string emailId { get; set; }
        public string mobileNumber { get; set; }
        public string myRefferalCode { get; set; }
        public string refferalCode { get; set; }
        public DateTime? birthDate { get; set; }
        public string status { get; set; }
        public System.Guid createdBy { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.Guid> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    }
}
