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
    public class ViajeService
    {
        private readonly IViajeRepository _repository;

        public ViajeService(IViajeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ViajeCreateResponse> Create(ViajeCreateRequest request)
        {
            return await _repository.Create(request);
        }

        public async Task<ViajeUpdateResponse> Update(ViajeUpdateRequest request)
        {
            return await _repository.Update(request);
        }

        public async Task<ViajeDeleteResponse> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<ViajeReadResponse>> GetAll(int companyId)
        {
            return await _repository.GetAll(companyId);
        }

        public async Task<IEnumerable<ViajeReadResponse>> GetAll(ViajeReadRequest request)
        {
            return await _repository.GetAll(request);
        }

        public async Task<ViajeReadResponse> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ViajeReadResponse> GetById(ViajeReadRequest request)
        {
            return await _repository.GetById(request);
        }

    }
}
