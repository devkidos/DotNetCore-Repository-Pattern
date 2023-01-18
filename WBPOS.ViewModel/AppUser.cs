using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WBPOS.ViewModel
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
        : base(principal)
        {
        }

        public string Name
        {
            get
            {
                var claimName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name);
                var value = (claimName == null ? string.Empty : claimName.Value);
                return value;
            }
        }

        public string UserType
        {
            get
            {
                var claimName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Actor);
                var value = (claimName == null ? string.Empty : claimName.Value);
                return value;
            }
        } 
        public string UserID
        {
            get
            {
                var claimName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid);
                var value = (claimName == null ? string.Empty : claimName.Value);
                return value;
            }
        }
        public string LoggedInTime
        {
            get
            {
                var claimName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.DateOfBirth);
                var value = (claimName == null ? string.Empty : claimName.Value);
                return value;
            }
        }
    }
}