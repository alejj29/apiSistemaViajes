using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain
{
    public class Ciudades
    {
        public int CiudadID { get; set; }
        public string NombreCiudad { get; set; }
        public string Pais { get; set; }
        public int? Poblacion { get; set; }
        public int? Status { get; set; }
    }


    public class CiudadesCreateRequest:Ciudades
    {
        public int IdCompany { get; set; }
        public int IYear { get; set; }
        public int? IMonth { get; set; }
        public string CreatedBy { get; set; }
        public string IpAddress { get; set; }
    }

    public class CiudadesCreateResponse : OperationResultBase
    {
        public int IdCiudad { get; set; }
    }

    public class CiudadesUpdateRequest:Ciudades
    {

    }

    public class CiudadesUpdateResponse : OperationResultBase
    {
    }
    public class CiudadesDeleteResponse : OperationResultBase
    {
    }

    public class CiudadesDeleteRequest
    {
        // Propiedades para la solicitud de eliminación de un usuario
        public int IdPeriod { get; set; }
    }

    public class CiudadesReadRequest
    {
        public int CompanyId { get; set; }
    }

    public class CiudadesReadResponse : Ciudades
    {

    }

    
}
