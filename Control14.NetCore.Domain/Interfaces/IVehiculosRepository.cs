using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain.Interfaces
{
    public interface IVehiculosRepository : IRepository<VehiculosCreateResponse, VehiculosUpdateResponse,
        VehiculosDeleteResponse, VehiculosReadResponse, VehiculosReadRequest, VehiculosCreateRequest,
        VehiculosUpdateRequest, VehiculosDeleteRequest>
    {

        
    }
}
