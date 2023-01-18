using System;
using System.Collections.Generic;
using System.Text;
using WBPOS.Data.Contracts;
using WBPOS.Entities;

namespace WBPOS.Data.Repositories
{ 
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(WBPOSContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
