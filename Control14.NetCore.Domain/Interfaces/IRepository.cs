using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain.Interfaces
{
    public interface IRepository<TCreateResponse, TUpdateResponse, TDeleteResponse, TReadResponse, TReadRequest, TCreateRequest, TUpdateRequest, TDeleteRequest>
    {
        Task<IEnumerable<TReadResponse>> GetAll(int id);
        Task<TReadResponse> GetById(int id);
        Task<IEnumerable<TReadResponse>> GetAll(TReadRequest request);
        Task<TReadResponse> GetById(TReadRequest request);
        Task<TCreateResponse> Create(TCreateRequest request);
        Task<TUpdateResponse> Update(TUpdateRequest request);
        Task<TDeleteResponse> Delete(TDeleteRequest request);
        Task<TDeleteResponse> Delete(int id);
    }

}
