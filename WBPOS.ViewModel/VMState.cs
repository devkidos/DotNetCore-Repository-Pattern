using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WBPOS.ViewModel
{

    public partial class VMState
    { 
        public decimal stateId { get; set; }

        [Display(Name = "Country Name")]
        public decimal countryId { get; set; }

        [Display(Name = "State Name")]
        public string stateName { get; set; }

        [Display(Name = "State Code")]
        public string stateCode { get; set; }
        public string status { get; set; }
        public System.Guid createdBy { get; set; }
        public System.DateTime createdDate { get; set; }
        public Nullable<System.Guid> updatedBy { get; set; }
        public System.DateTime updatedDate { get; set; }
        public List<ddlList> countryList { get; set; }
    }
}
