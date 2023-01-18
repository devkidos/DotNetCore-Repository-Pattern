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
    public class CountryService : ICountryService
    {
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CountryService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultResponse<VMCountry>> Delete(object id)
        {
            ResultResponse<VMCountry> response = new ResultResponse<VMCountry>();
            var exceptions = new Dictionary<string, string>();

            try
            {
                var entity = await _repository.Country.FindByCondition(a => a.countryId == (decimal)id);

                var model = entity.FirstOrDefault();
                model.status = "Deleted";
                var data = await _repository.Country.Update(model);

                var vmData = _mapper.Map<VMCountry>(data);
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

        public async Task<ResultResponse<List<VMCountry>>> GetData()
        {
            ResultResponse<List<VMCountry>> response = new ResultResponse<List<VMCountry>>();
            var data = await _repository.Country.FindAll();

            List<VMCountry> vmData = _mapper.Map<List<VMCountry>>(data.ToList());

            response.Datas = vmData;
            return response;
        }

        public async Task<ResultResponse<DTResult<VMCountry>>> GetCountryList(DTParameters param)
        {
            var exceptions = new Dictionary<string, string>();
            DTResult<VMCountry> Data = new DTResult<VMCountry>();

            try
            {
                //var SortColumn = StringExtension.FirstCharToUpper(param.SortOrder);
                //param.SortOrder = SortColumn;
                var queryData = _repository.Country.GetAll()
                    .Where(a => (a.countryName.Contains(param.Search.Value) || param.Search.Value == null));

                var data = DataTableFiltering<Country>.GetResult(param, queryData);

                var Datas = _mapper.Map<List<VMCountry>>(data.data);
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
            return new ResultResponse<DTResult<VMCountry>>
            {
                Exceptions = exceptions,
                Datas = Data
            };
        }

        public async Task<ResultResponse<VMCountry>> GetDataById(object id)
        {
            ResultResponse<VMCountry> response = new ResultResponse<VMCountry>();
            var entity = await _repository.Country.FindByCondition(a => a.countryId == (decimal)id);

            var model = entity.FirstOrDefault();

            var vmData = _mapper.Map<VMCountry>(model);
            response.Datas = vmData;
            return response;
        }

        public async Task<ResultResponse<VMCountry>> Insert(VMCountry entity, string UserId = null)
        {
            var exceptions = new Dictionary<string, string>();
            ResultResponse<VMCountry> data = new ResultResponse<VMCountry>();

            try
            {
                var duplicate = await _repository.Country.FindByCondition(a => a.countryName == entity.countryName);
                if (duplicate.FirstOrDefault() != null)
                {
                    data.Success = false;
                    data.Message = "Country is already exist";
                }
                else
                {
                    var model = _mapper.Map<Country>(entity);

                    var id = Guid.NewGuid();
                    model.status = Status.Active.ToString();
                    model.createdDate = DateTime.Now;
                    model.updatedDate = DateTime.Now;
                    var userid = new Guid(UserId);
                    model.createdBy = userid;

                    var result = await _repository.Country.Insert(model);

                    entity.countryId = result.countryId;
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

        public async Task<ResultResponse<VMCountry>> Update(VMCountry entity, string UserId = null)
        {
            ResultResponse<VMCountry> response = new ResultResponse<VMCountry>();
            var model = _mapper.Map<Country>(entity);
            var eData = await _repository.Country.FindByCondition(a => a.countryId == entity.countryId);
            var editData = eData.FirstOrDefault();
            editData.countryName = entity.countryName;
            editData.countryCode = entity.countryCode;
            editData.status = entity.status;
            var id = Guid.NewGuid();
            editData.updatedDate = DateTime.Now;
            var userid = new Guid(UserId);
            editData.updatedBy = userid;

            var data = await _repository.Country.Update(editData);
            response.Datas = entity;
            return response;

        }

        public async Task<ResultResponse<List<VMCountry>>> GetList()
        {
            ResultResponse<List<VMCountry>> response = new ResultResponse<List<VMCountry>>();
            var data = await _repository.Country.FindAll();

            List<VMCountry> vmData = _mapper.Map<List<VMCountry>>(data.ToList());

            response.Datas = vmData;
            return response;
        }

        public async Task<ResultResponse<List<ddlList>>> GetCountryListForDDL()
        {
            ResultResponse<List<ddlList>> response = new ResultResponse<List<ddlList>>();
            var exceptions = new Dictionary<string, string>();

            try
            {
                var entity = await _repository.Country.FindAll();
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

