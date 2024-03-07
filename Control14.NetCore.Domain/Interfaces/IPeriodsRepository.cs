using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain.Interfaces
{
    public interface IPeriodsRepository : IRepository<PeriodsCreateResponse, PeriodsUpdateResponse, PeriodsDeleteResponse, PeriodsReadResponse, PeriodsReadRequest, PeriodsCreateRequest, PeriodsUpdateRequest, PeriodsDeleteRequest>
    {
        Task<OperationResultBase> UpdateStatus(PeriodsUpdateStatusRequest request);
        Task<OperationResultBase> UpdateStatusProgramming(PeriodsUpdateStatusProgrammingRequest request);
        
    }
}
