using AutoMapper;
using DevKido.Utilities.Core;
using DevKido.Utilities.Core.DataTable;
using WBPOS.Data.Contracts;
using WBPOS.Entities;
using WBPOS.Services.Contracts;
using WBPOS.Services.Helpers;
using WBPOS.ViewModel;
using WBPOS.ViewModel.Request;
using WBPOS.ViewModel.Response;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WBPOS.Services.Service
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        private Random _random = new Random();
        public UserService(IRepositoryWrapper repository, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _repository = repository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ResultResponse<List<VMUser>>> GetData()
        {
            ResultResponse<List<VMUser>> response = new ResultResponse<List<VMUser>>();
            var data = await _repository.User.FindAll();

            List<VMUser> vmData = _mapper.Map<List<VMUser>>(data.ToList());

            response.Datas = vmData;
            return response;
        }

        public async Task<ResultResponse<VMUser>> GetDataById(object id)
        {
            ResultResponse<VMUser> response = new ResultResponse<VMUser>();
            var exceptions = new Dictionary<string, string>(); 

            try
            {
                var entity = await _repository.User.FindByCondition(a => a.userId == (Guid)id);

                var model = entity.FirstOrDefault();

                var vmData = _mapper.Map<VMUser>(model);
                response.Datas = vmData;
            }
            catch (SqlException sqlException)
            {
                exceptions.Add("SqlException", sqlException.Message);
                response.Success = false;
            }
            catch (TaskCanceledException taskCanceledException)
            {
                exceptions.Add("TaskCanceledException", taskCanceledException.Message);
                response.Success = false;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Success = false;
            }

            response.Exceptions = exceptions;
            return response;
        }

        public async Task<ResultResponse<VMUser>> Insert(VMUser entity, string UserId = null)
        {
            ResultResponse<VMUser> response = new ResultResponse<VMUser>();
            var model = _mapper.Map<User>(entity);
            var data = await _repository.User.Insert(model);
            response.Datas = entity;
            return response;
        }

        public async Task<ResultResponse<VMUser>> Update(VMUser entity, string UserId = null)
        {
            ResultResponse<VMUser> response = new ResultResponse<VMUser>();
            var model = _mapper.Map<User>(entity);

            var eData = await _repository.User.FindByCondition(a => a.userId == entity.userId);
            var editData = eData.FirstOrDefault();

            editData.username = entity.username;
            editData.emailId = entity.emailId;

            editData.updatedDate = DateTime.Now;
            var userid = new Guid(UserId);
            editData.updatedBy = userid;

            var data = await _repository.User.Update(editData);
            response.Datas = entity;
            return response;
        }

        public async Task<ResultResponse<VMUser>> Delete(object id)
        {
            ResultResponse<VMUser> response = new ResultResponse<VMUser>();
            var entity = await _repository.User.FindByCondition(a => a.userId == (Guid)id);

            var model = entity.FirstOrDefault();
            model.status = "Deleted";
            var data = await _repository.User.Update(model);

            var vmData = _mapper.Map<VMUser>(data);
            response.Datas = vmData;

            return response;
        }

        public async Task<ResultResponse<VMUser>> Authenticate(AuthenticateRequest model, string userType)
        {
            var exceptions = new Dictionary<string, string>();
            ResultResponse<VMUser> response = new ResultResponse<VMUser>();

            try
            {

                var user = await _repository.User.FindByCondition(x => (x.username == model.Username || x.emailId== model.Username) && x.password == Cryptography.Encrypt(model.Password) && x.usertype == userType);
           
                
                var userdata = user.FirstOrDefault();
                // return null if user not found
                if (userdata == null)
                {
                    response.Success = false;
                    throw new Exception("User data not found.");                   
                }
                
                var vmData = _mapper.Map<VMUser>(userdata);
                    response.Datas = vmData;
            }
            catch (SqlException sqlException)
            {
                exceptions.Add("SqlException", sqlException.Message);
                response.Success = false;
            }
            catch (TaskCanceledException taskCanceledException)
            {
                exceptions.Add("TaskCanceledException", taskCanceledException.Message);
                response.Success = false;
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
                response.Success = false;
            }

            response.Exceptions = exceptions;
            return response; 
        }

       

        public async Task<ResultResponse<VMUsers>> GetUserData(string id)
        {
            ResultResponse<VMUsers> response = new ResultResponse<VMUsers>();
            var exceptions = new Dictionary<string, string>();

            try
            {
                var entity = await _repository.User.FindByCondition(a => a.userId == new Guid(id));

                var model = entity.FirstOrDefault();

                var vmData = _mapper.Map<VMUsers>(model);
                response.Datas = vmData;
            }
            catch (SqlException sqlException)
            {
                exceptions.Add("SqlException", sqlException.Message);
                response.Success = false;
            }
            catch (TaskCanceledException taskCanceledException)
            {
                exceptions.Add("TaskCanceledException", taskCanceledException.Message);
                response.Success = false;
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null)
                      ? ex.InnerException.Message
                      : "";

                var exception = ex.Message;
                if (!string.IsNullOrEmpty(innerMessage))
                    exception += " - inner message = " + innerMessage;

                exceptions.Add("Exception", exception);
                response.Success = false;
            }

            response.Exceptions = exceptions;
            return response;
        }

        public async Task<ResultResponse<VMUsers>> UpdateUserData(VMUsers entity, string userId)
        {
           ResultResponse<VMUsers> response = new ResultResponse<VMUsers>();
           var exceptions = new Dictionary<string, string>();
           
            try
            {
                var duplicate = await _repository.User.FindByCondition(a => a.mobileNumber == entity.mobileNumber && a.userId != new Guid(userId));

                if (duplicate.FirstOrDefault() != null)
                {
                    response.Message = "Mobile number is already registered";
                    response.Success = false;
                }
                else
                {
                    var user = await _repository.User.FindByCondition(o => o.userId == new Guid(userId));
                    var userData = user.FirstOrDefault(); 
                    userData.updatedDate = DateTime.Now;
                    userData.firstName = entity.firstName;
                    userData.sex = entity.sex;
                    userData.emailId = entity.emailId;
                    if(!string.IsNullOrEmpty(entity.mobileNumber))
                    {
                        userData.mobileNumber = entity.mobileNumber;                       
                    }                    
                    userData.birthDate = entity.birthDate;
                    userData.myRefferalCode = entity.myRefferalCode;
                    userData.status = entity.status;

                    var userid = new Guid(userId);
                    userData.updatedDate = DateTime.Now;
                    userData.updatedBy = userid;

                    var data = await _repository.User.Update(userData);

                    response.Datas = entity;
                }
            }
            catch (SqlException sqlException)
            {
                exceptions.Add("SqlException", sqlException.Message);
                response.Success = false;
            }
            catch (TaskCanceledException taskCanceledException)
            {
                exceptions.Add("TaskCanceledException", taskCanceledException.Message);
                response.Success = false;
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null)
                       ? ex.InnerException.Message
                       : "";

                var exception = ex.Message;
                if (!string.IsNullOrEmpty(innerMessage))
                    exception += " - inner message = " + innerMessage;

                exceptions.Add("Exception", exception);
                response.Success = false;
            }

            response.Exceptions = exceptions;
            return response;
        }

       
        public async Task<ResultResponse<DTResult<VMUser>>> GetUsersList(DTParameters param)
        {
            var exceptions = new Dictionary<string, string>();
            DTResult<VMUser> Data = new DTResult<VMUser>();

            try
            {
                //var SortColumn = StringExtension.FirstCharToUpper(param.SortOrder);
                //param.SortOrder = SortColumn;
                var queryData = _repository.User.GetAll()
                    .Where(a => a.usertype != "admin" && (a.username.Contains(param.Search.Value) || param.Search.Value == null));

                var data = DataTableFiltering<User>.GetResult(param, queryData);

                var Datas = _mapper.Map<List<VMUser>>(data.data);
                Data.data = Datas;
                Data.draw = data.draw;
                Data.recordsTotal = data.recordsTotal;
            }
            catch (SqlException sqlException)
            {
                exceptions.Add("SqlException", sqlException.Message);
            }
            catch (TaskCanceledException taskCanceledException)
            {
                exceptions.Add("TaskCanceledException", taskCanceledException.Message);
            }
            catch (Exception ex)
            {
                exceptions.Add("Exception", ex.Message);
            }
            return new ResultResponse<DTResult<VMUser>>
            {
                Exceptions = exceptions,
                Datas = Data
            };
        } 
    }
}
