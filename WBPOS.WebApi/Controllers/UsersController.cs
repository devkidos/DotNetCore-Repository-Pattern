using DevKido.Utilities.Core;
using WBPOS.Services.Contracts;
using WBPOS.ViewModel;
using WBPOS.ViewModel.Request;
using WBPOS.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WBPOS.WebApi.Controllers
{ 
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IServiceWrapper service;
        public IConfiguration _configuration;
        public UsersController(IConfiguration config, IServiceWrapper _service)
        {
            _configuration = config;
            service = _service;
        } 

        [HttpGet]
        [Route("Users/All")]
        public async Task<IActionResult> GetAll()
        {
            var users = await service.User.GetData();
            return Ok(users);
        }

        [HttpGet]
        [Route("Users/GetUserDetail")]
        public async Task<IActionResult> GetUserDetail()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("Id")?.Value;

            var user = await service.User.GetUserData(userId);
            return Ok(user);
        }

        [HttpPost]
        [Route("Users/UpdateUserDetail")]
        public async Task<IActionResult> UpdateUserDetail(VMUsers vmUser)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("Id")?.Value;

            var user = await service.User.UpdateUserData(vmUser, userId);
            return Ok(user);
        }

        [Authorize]
        [HttpPost("Users/UpdateUserProfilePicture")]
        public async Task<IActionResult> UpdateUserProfilePicture([FromForm] VMUserUploadPhoto model)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("Id")?.Value;

            var data = await service.User.UpdateUserProfilePicture(model, userId);
            return Ok(data);
        }

        [HttpPost]
        [Route("Users/ChangePassword")]
        public async Task<IActionResult> ChangePassword(VMUserChangePassword model)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("Id")?.Value;

            var user = await service.User.ChangePassword(model, userId);
            return Ok(user);
        }
         
        [AllowAnonymous]
        [HttpPost]
        [Route("Users/ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(VMForgotPassword model)
        { 
            var user = await service.User.ForgotPassword(model);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Users/ResetPassword")]
        public async Task<IActionResult> ResetPassword(VMResetPassword model)
        {            
            var user = await service.User.ResetPassword(model);
            return Ok(user);
        }


        [HttpPost]
        [Route("Users/FollowUser")]
        public async Task<IActionResult> FollowUser(VMFollows model)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("Id")?.Value;

            var user = await service.Follows.FollowUser(model, userId);
            return Ok(user);
        }

        [HttpGet]
        [Route("Users/FollowUserCount")]
        public async Task<IActionResult> FollowUserCount()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("Id")?.Value;

            var user = await service.Follows.FollowUserCount(userId);
            return Ok(user);
        } 

        [AllowAnonymous]
        [HttpPost]
        [Route("Users/RegisterUser")]
        public async Task<IActionResult> RegisterUser(VMRegister user)
        {
            var data = await service.User.Register(user);

            if(data.Datas != null)
            {
                var _userData = new AuthenticateRequest { Username = user.MobileNumber, Password = user.Password };

                // Generate Token
                 
                    var userResponse = await service.User.Authenticate(_userData, "user");

                    if (user != null && userResponse.Exceptions.Count == 0)
                    {
                        //create claims details based on the user information
                        var claims = new[] {
                     new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim(JwtRegisteredClaimNames.Sid, userResponse.Datas.userId.ToString()),
                     new Claim(JwtRegisteredClaimNames.Birthdate, DateTime.UtcNow.ToString()),
                     new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                     new Claim("Id", userResponse.Datas.userId.ToString()),
                     new Claim("FirstName", userResponse.Datas.firstName),
                     new Claim("UserName", userResponse.Datas.username)
                    };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: signIn);

                        var tokens = new JwtSecurityTokenHandler().WriteToken(token).ToString();

                    data.Message = tokens;
                } 
                
            }
                return Ok(data);
             
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Users/VerifyOTP")]
        public async Task<IActionResult> VerifyOTP(UserOTP userOTP)
        {
            var data = await service.User.VerifyOTP(userOTP);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Users/password")]
        public IActionResult password(string userPassword)
        {
            var data = Cryptography.Decrypt(userPassword);
            return Ok(data);
        }
    }
}
