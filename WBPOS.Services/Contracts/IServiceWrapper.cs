using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.Services.Contracts
{
    public interface IServiceWrapper
    {
        ICountryService Country { get; }
        IUserService User { get; } 
        IStateService State { get; } 
    }
}
