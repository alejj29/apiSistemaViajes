using Control14.NetCore.Domain;
using Control14.NetCore.Domain.Interfaces;
using DocumentFormat.OpenXml.Office2016.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.BusinessLogic.Services
{
    public class PeriodsService
    {
        private readonly IPeriodsRepository _repository;

        public PeriodsService(IPeriodsRepository repository)
        {
            _repository = repository;
        }

        public async Task<PeriodsCreateResponse> Create(PeriodsCreateRequest request)
        {
            return await _repository.Create(request);
        }

        public async Task<PeriodsUpdateResponse> Update(PeriodsUpdateRequest request)
        {
            return await _repository.Update(request);
        }

        public async Task<PeriodsDeleteResponse> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<PeriodsReadResponse>> GetAll(int companyId)
        {
            return await _repository.GetAll(companyId);
        }

        public async Task<IEnumerable<PeriodsReadResponse>> GetAll(PeriodsReadRequest request)
        {
            return await _repository.GetAll(request);
        }

        public async Task<PeriodsReadResponse> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<PeriodsReadResponse> GetById(PeriodsReadRequest request)
        {
            return await _repository.GetById(request);
        }
        public async Task<OperationResultBase> UpdateStatus(PeriodsUpdateStatusRequest request) 
        {
            return await _repository.UpdateStatus(request);
        }

        public async Task<OperationResultBase> UpdateStatusProgramming(PeriodsUpdateStatusProgrammingRequest request)
        {
            return await _repository.UpdateStatusProgramming(request);
        }
    }
}
