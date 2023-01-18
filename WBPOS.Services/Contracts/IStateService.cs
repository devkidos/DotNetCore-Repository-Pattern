using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevKido.Utilities.Core;
using DevKido.Utilities.Core.DataTable;
using WBPOS.ViewModel;

namespace WBPOS.Services.Contracts
{
    public interface IStateService : IService<VMState>
    {
        Task<ResultResponse<DTResult<VMState>>> GetStateList(DTParameters param);
        Task<ResultResponse<List<ddlList>>> GetStateListForDDL();
    }
}
