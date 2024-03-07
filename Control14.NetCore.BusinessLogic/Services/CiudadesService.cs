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
    public class CiudadesService
    {
        private readonly ICiudadesRepository _repository;

        public CiudadesService(ICiudadesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CiudadesCreateResponse> Create(CiudadesCreateRequest request)
        {
            return await _repository.Create(request);
        }

        public async Task<CiudadesUpdateResponse> Update(CiudadesUpdateRequest request)
        {
            return await _repository.Update(request);
        }

        public async Task<CiudadesDeleteResponse> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<CiudadesReadResponse>> GetAll(int companyId)
        {
            return await _repository.GetAll(companyId);
        }

        public async Task<IEnumerable<CiudadesReadResponse>> GetAll(CiudadesReadRequest request)
        {
            return await _repository.GetAll(request);
        }

        public async Task<CiudadesReadResponse> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<CiudadesReadResponse> GetById(CiudadesReadRequest request)
        {
            return await _repository.GetById(request);
        }
    }
}
