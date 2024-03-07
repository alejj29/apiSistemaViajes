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
    public class VehiculosService
    {
        private readonly IVehiculosRepository _repository;

        public VehiculosService(IVehiculosRepository repository)
        {
            _repository = repository;
        }

        public async Task<VehiculosCreateResponse> Create(VehiculosCreateRequest request)
        {
            return await _repository.Create(request);
        }

        public async Task<VehiculosUpdateResponse> Update(VehiculosUpdateRequest request)
        {
            return await _repository.Update(request);
        }

        public async Task<VehiculosDeleteResponse> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<VehiculosReadResponse>> GetAll(int companyId)
        {
            return await _repository.GetAll(companyId);
        }

        public async Task<IEnumerable<VehiculosReadResponse>> GetAll(VehiculosReadRequest request)
        {
            return await _repository.GetAll(request);
        }

        public async Task<VehiculosReadResponse> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<VehiculosReadResponse> GetById(VehiculosReadRequest request)
        {
            return await _repository.GetById(request);
        }

    }
}
