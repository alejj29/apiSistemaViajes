using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain.Interfaces
{
    public interface ICiudadesRepository : IRepository<CiudadesCreateResponse, CiudadesUpdateResponse, 
        CiudadesDeleteResponse, CiudadesReadResponse, CiudadesReadRequest, CiudadesCreateRequest,
        CiudadesUpdateRequest, CiudadesDeleteRequest>
    {
        
    }
}
