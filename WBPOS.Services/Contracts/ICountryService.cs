using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevKido.Utilities.Core;
using DevKido.Utilities.Core.DataTable;
using WBPOS.ViewModel;

namespace WBPOS.Services.Contracts
{
    public interface ICountryService : IService<VMCountry>
    {
        Task<ResultResponse<DTResult<VMCountry>>> GetCountryList(DTParameters param);
        Task<ResultResponse<List<ddlList>>> GetCountryListForDDL();
    }
}
