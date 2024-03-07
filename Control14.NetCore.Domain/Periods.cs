using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain
{
    public class Periods
    {
        public int IdCompany { get; set; }
        public int IdPeriod { get; set; }
        public int IYear { get; set; }
        public int? IMonth { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string PeriodCode { get; set; }
        public int? IdStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string IpAddress { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string ClosedBy { get; set; }        
        public string MonthName { get; set; }
        public string StatusName { get; set; }
    }


    public class PeriodsCreateRequest
    {
        public int IdCompany { get; set; }
        public int IYear { get; set; }
        public int? IMonth { get; set; }
        public string CreatedBy { get; set; }
        public string IpAddress { get; set; }
    }

    public class PeriodsCreateResponse : OperationResultBase
    {
        public int IdPeriod { get; set; }
    }

    public class PeriodsUpdateRequest
    {
        public int IdCompany { get; set; }
        public int IdPeriod { get; set; }
        public int IYear { get; set; }
        public int? IMonth { get; set; }
        public string UpdatedBy { get; set; }
        public string IpAddress { get; set; }
    }

    public class PeriodsUpdateResponse : OperationResultBase
    {
    }
    public class PeriodsDeleteResponse : OperationResultBase
    {
    }

    public class PeriodsDeleteRequest
    {
        // Propiedades para la solicitud de eliminación de un usuario
        public int IdPeriod { get; set; }
    }

    public class PeriodsReadRequest
    {
        public int CompanyId { get; set; }
    }

    public class PeriodsReadResponse : Periods
    {

    }

    public class PeriodsUpdateStatusRequest
    {
        public int IdPeriod { get; set; }
        public string UpdatedBy { get; set; }
        public string Action { get; set; }
        public int CompanyId { get; set; }
       
    }

    public class PeriodsUpdateStatusProgrammingRequest
    {
        public int CompanyId { get; set; }
        public DateTime DateClosingStart { get; set; }
        public DateTime DateClosingEnd { get; set; }        
        public string UpdatedBy { get; set; }
        public string Action { get; set; }
 

    }
    
}
