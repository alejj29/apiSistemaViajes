using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain
{
    public class Viaje
    {
        public int ViajeID { get; set; }
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public int? VehiculoID { get; set; }
        public int? CiudadOrigenID { get; set; }
        public int? CiudadDestinoID { get; set; }
        public int? Status { get; set; }
    }


    public class ViajeCreateRequest:Viaje
    {
 
    }

    public class ViajeCreateResponse : OperationResultBase
    {
        public int IdViaje { get; set; }
    }

    public class ViajeUpdateRequest:Viaje
    {
 
    }

    public class ViajeUpdateResponse : OperationResultBase
    {
    }
    public class ViajeDeleteResponse : OperationResultBase
    {
    }

    public class ViajeDeleteRequest
    {
        // Propiedades para la solicitud de eliminación de un usuario
        public int IdPeriod { get; set; }
    }

    public class ViajeReadRequest
    {
        public int CompanyId { get; set; }
    }

    public class ViajeReadResponse : Viaje
    {


        public int? ViajeID { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int? VehiculoID { get; set; }
        public string TipoVehiculo { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string CiudadOrigen { get; set; }
    
        public string CiudadDestino { get; set; }

        public int Status { get; set; }
    }


    
}
