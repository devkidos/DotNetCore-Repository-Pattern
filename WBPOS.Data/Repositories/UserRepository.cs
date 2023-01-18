using WBPOS.Data.Contracts;
using WBPOS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.Data.Repositories
{   
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WBPOSContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
