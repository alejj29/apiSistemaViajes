using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain
{
    public class Vehiculos
    {
        public int VehiculoID { get; set; }
        public string TipoVehiculo { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public int? Status { get; set; }
    }


    public class VehiculosCreateRequest:Vehiculos
    {
   
    }

    public class VehiculosCreateResponse : OperationResultBase
    {
        public int VehiculoID { get; set; }
    }

    public class VehiculosUpdateRequest:Vehiculos
    {
    
    }

    public class VehiculosUpdateResponse : OperationResultBase
    {
    }
    public class VehiculosDeleteResponse : OperationResultBase
    {
    }

    public class VehiculosDeleteRequest
    {
        // Propiedades para la solicitud de eliminación de un usuario
        public int IdPeriod { get; set; }
    }

    public class VehiculosReadRequest
    {
        public int CompanyId { get; set; }
    }

    public class VehiculosReadResponse : Vehiculos
    {

    }


    
}
