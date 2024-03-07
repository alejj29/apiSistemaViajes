namespace Control14.NetCore.Common
{
    public class Constants
    {
        public const int IdEstadoPendiente = 2027;
        public const int IdEstadoEnviado= 2028;
        public const int IdEstadoAprobado= 2030;
        public const int IdEstadoRechazado = 2031;
        public const int IdEstadoObservado = 2032;
        public static string Key = "KaitlynGavidiaAsalde2019$";

        public enum PeriodStatus
        {
            Open = 1,
            Closed = 0,
        }

        public enum ExceptionCycleLengths
        {
            RangoFechas = 1,
            RangoHoras = 2,
            HoraExacta = 3,
        }
    }
}
