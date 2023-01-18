using AutoMapper;
using WBPOS.Data.Contracts;
using WBPOS.Services.Contracts;
using WBPOS.Services.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.Services.Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        private ICountryService _country;
        private IUserService _user; 
        private IStateService _state; 
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment; 
        private IConfiguration configuration { get; }

        public ServiceWrapper(IRepositoryWrapper repository, IMapper mapper, IWebHostEnvironment hostingEnvironment, IConfiguration Configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            configuration = Configuration;
        }
         
        public ICountryService Country
        {
            get
            {
                if (_country == null)
                {
                    _country = new CountryService(_repository, _mapper);
                }
                return _country;
            }
        }

        public IUserService User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserService(_repository, _mapper, _hostingEnvironment);
                }
                return _user;
            }
        } 

        public IStateService State
        {
            get
            {
                if (_state == null)
                {
                    _state = new StateService(_repository, _mapper);
                }
                return _state;
            }
        }  
    }
}
