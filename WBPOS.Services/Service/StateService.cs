using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevKido.Utilities.Core;
using DevKido.Utilities.Core.DataTable;
using WBPOS.Data.Contracts;
using WBPOS.Entities;
using WBPOS.Services.Contracts;
using WBPOS.Services.Helpers;
using WBPOS.ViewModel;
using Microsoft.Data.SqlClient;

namespace WBPOS.Services.Service
{
    public class StateService : IStateService
    {
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public StateService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultResponse<VMState>> Delete(object id)
        {
            ResultResponse<VMState> response = new ResultResponse<VMState>();
            var entity = await _repository.State.FindByCondition(a => a.stateId == (decimal)id);

            var model = entity.FirstOrDefault();
            model.status = "Deleted";
            var data = await _repository.State.Update(model);

            var vmData = _mapper.Map<VMState>(data);
            response.Datas = vmData;

            return response;
        }

        public async Task<ResultResponse<List<VMState>>> GetData()
        {
            ResultResponse<List<VMState>> response = new ResultResponse<List<VMState>>();
            var data = await _repository.State.FindAll();

            List<VMState> vmData = _mapper.Map<List<VMState>>(data.ToList());

            response.Datas = vmData;
            return response;
        }


        public async Task<ResultResponse<VMState>> GetDataById(object id)
        {
            ResultResponse<VMState> response = new ResultResponse<VMState>();
            var entity = await _repository.State.FindByCondition(a => a.stateId == (decimal)id);

            var model = entity.FirstOrDefault();

            var vmData = _mapper.Map<VMState>(model);
            response.Datas = vmData;
            return response;
        }

        public async Task<ResultResponse<VMState>> Insert(VMState entity, string UserId = null)
        {
            var exceptions = new Dictionary<string, string>();
            ResultResponse<VMState> data = new ResultResponse<VMState>();

            try
            {
                var duplicate = await _repository.State.FindByCondition(a => a.stateName == entity.stateName);
                if (duplicate.FirstOrDefault() != null)
                {
                    data.Success = false;
                    data.Message = "State is already exist";
                }
                else
                {
                    var model = _mapper.Map<State>(entity);

                    var id = Guid.NewGuid();
                    model.status = Status.Active.ToString();
                    model.createdDate = DateTime.Now;
                    model.updatedDate = DateTime.Now;
                    var userid = new Guid(UserId);
                    model.createdBy = userid;

                    var result = await _repository.State.Insert(model);

                    entity.stateId = result.stateId;
                    data.Message = Message.Success.ToString();
                    data.Datas = entity;
                }
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

            data.Exceptions = exceptions;
            return data;

        }

        public async Task<ResultResponse<VMState>> Update(VMState entity, string UserId = null)
        {
            ResultResponse<VMState> response = new ResultResponse<VMState>();
            var model = _mapper.Map<State>(entity);
            var data = await _repository.State.Update(model);
            response.Datas = entity;
            return response;

        }

        public async Task<ResultResponse<DTResult<VMState>>> GetStateList(DTParameters param)
        {
            var exceptions = new Dictionary<string, string>();
            DTResult<VMState> Data = new DTResult<VMState>();

            try
            {
                //var SortColumn = StringExtension.FirstCharToUpper(param.SortOrder);
                //param.SortOrder = SortColumn;
                var queryData = _repository.State.GetAll()
                    .Where(a => (a.stateName.Contains(param.Search.Value) || param.Search.Value == null));

                var data = DataTableFiltering<State>.GetResult(param, queryData);

                var Datas = _mapper.Map<List<VMState>>(data.data);
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
            return new ResultResponse<DTResult<VMState>>
            {
                Exceptions = exceptions,
                Datas = Data
            };
        }

        public async Task<ResultResponse<List<ddlList>>> GetStateListForDDL()
        {
            ResultResponse<List<ddlList>> response = new ResultResponse<List<ddlList>>();
            var exceptions = new Dictionary<string, string>();

            try
            {
                var entity = await _repository.State.FindAll();
                var data = entity.ToList();

                var vmData = _mapper.Map<List<ddlList>>(data);
                response.Datas = vmData;

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

            response.Exceptions = exceptions;
            return response;
        }
    }
}
