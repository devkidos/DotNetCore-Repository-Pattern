using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.Data.Contracts
{
    public interface IRepositoryWrapper
    {
        ICountryRepository Country { get; }
        IStateRepository State { get; } 
        IUserRepository User { get; } 

    }
}
