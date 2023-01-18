using WBPOS.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private WBPOSContext _repoContext;
        private ICountryRepository _country;
        private IStateRepository _state; 
        private IUserRepository _users; 
         
        public RepositoryWrapper(WBPOSContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public ICountryRepository Country
        {
            get
            {
                if (_country == null)
                {
                    _country = new CountryRepository(_repoContext);
                }
                return _country;
            }
        }
        public IStateRepository State
        {
            get
            {
                if (_state == null)
                {
                    _state = new StateRepository(_repoContext);
                }
                return _state;
            }
        } 
        public IUserRepository User
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_repoContext);
                }
                return _users;
            }
        } 
         
    }
}
