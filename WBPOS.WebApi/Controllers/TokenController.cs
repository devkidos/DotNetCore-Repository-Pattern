using DevKido.Utilities.Core;
using WBPOS.Services.Contracts;
using WBPOS.ViewModel;
using WBPOS.ViewModel.Request;
using WBPOS.ViewModel.Response;
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
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private IServiceWrapper service;

        public TokenController(IConfiguration config, IServiceWrapper _service)
        {
            service = _service;
            _configuration = config; 
        }

        [HttpPost]
        [Route("Users/token")]
        public async Task<IActionResult> Post(AuthenticateRequest _userData)
        {
            if (_userData != null)
            {
                var user = await service.User.Authenticate(_userData, "user");

                if (user != null && user.Exceptions.Count == 0)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                     new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim(JwtRegisteredClaimNames.Sid, user.Datas.userId.ToString()),
                     new Claim(JwtRegisteredClaimNames.Birthdate, DateTime.UtcNow.ToString()),
                     new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                     new Claim("Id", user.Datas.userId.ToString()),
                     new Claim("FirstName", user.Datas.firstName), 
                     new Claim("UserName", user.Datas.username)  
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: signIn);

                    var tokens = new JwtSecurityTokenHandler().WriteToken(token).ToString(); 
                     
                    return Ok(new AuthenticateResponse(user.Datas, tokens)); 
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("Users/SocialMediaToken")]
        public async Task<IActionResult> SocialMediaToken(VMUserSocialMedia _userData)
        {
            if (_userData != null)
            {
                var user = await service.User.SocialMediaToken(_userData);

                if (user != null && user.Exceptions.Count == 0)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                     new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim(JwtRegisteredClaimNames.Sid, user.Datas.userId.ToString()),
                     new Claim(JwtRegisteredClaimNames.Birthdate, DateTime.UtcNow.ToString()),
                     new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                     new Claim("Id", user.Datas.userId.ToString()),
                     new Claim("FirstName", user.Datas.firstName),
                     new Claim("UserName", user.Datas.username)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: signIn);

                    var tokens = new JwtSecurityTokenHandler().WriteToken(token).ToString();

                    return Ok(new AuthenticateResponse(user.Datas, tokens));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
         
    }
}
