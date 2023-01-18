using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.ViewModel.Response
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(VMUser user, string token)
        {
            Id = user.userId;
            FirstName = user.firstName;
            LastName = user.lastName;
            Username = user.username;
            Token = token;
        }
    }
}
