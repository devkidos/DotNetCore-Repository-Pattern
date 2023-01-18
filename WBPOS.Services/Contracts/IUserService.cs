using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WBPOS.Entities; 
using WBPOS.ViewModel;
using WBPOS.ViewModel.Response;
using WBPOS.ViewModel.Request;
using System.Threading.Tasks;
using DevKido.Utilities.Core;
using DevKido.Utilities.Core.DataTable;

namespace WBPOS.Services.Contracts
{
    public interface IUserService : IService<VMUser>
    {
        Task<ResultResponse<VMUser>> Authenticate(AuthenticateRequest model, string userType); 
        Task<ResultResponse<VMUsers>> GetUserData(string id);
        Task<ResultResponse<VMUsers>> UpdateUserData(VMUsers entity, string userId);  
        Task<ResultResponse<DTResult<VMUser>>> GetUsersList(DTParameters param);
    }
}
