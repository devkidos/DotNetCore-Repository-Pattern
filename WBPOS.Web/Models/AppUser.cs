using System.Security.Claims;

namespace WBPOS.Web.Models
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
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Country
        {
            get
            {
                return this.FindFirst(ClaimTypes.Country).Value;
            }
        }
        public string UserID
        {
            get
            {
                return this.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
        public string CreatedDate
        {
            get
            {
                return this.FindFirst(ClaimTypes.DateOfBirth).Value;
            }
        }
         
    }
}