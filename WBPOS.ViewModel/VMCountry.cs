using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.ViewModel
{

    public partial class VMCountry
    { 
        public decimal countryId { get; set; }
        public string countryName { get; set; }
        public string countryCode { get; set; }
        public string countryPhoneCode { get; set; }
        public string status { get; set; }
        public System.Guid createdBy { get; set; }
        public System.DateTime createdDate { get; set; }
        public Nullable<System.Guid> updatedBy { get; set; }
        public System.DateTime updatedDate { get; set; }
        public List<ddlList> statusList { get; set; }
    }
}
