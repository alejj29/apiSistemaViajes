using System;
using System.Collections.Generic;
using System.Data;

using System.Windows;
using System.IO;
using System.Reflection;

namespace Control14.NetCore.Common
{
    public class Variables
    {
        public static string CorreosAdministrativos = "agavidiad@gmail.com";
        public static string LlaveSeguridad = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";        
        public static string baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string SAPSOLUTIONSini = baseDir + "\\System\\debug.ini\\SAPSOLUTIONS.ini";
        public static string Finalizar = baseDir + "\\System\\debug.ini\\TTTX\\YYZ\\HHH\\GGG\\asdfgh.ini";
        public static string Folderdll = baseDir + "\\System\\dll";
        public static string AplicacionComunicacion = baseDir + "\\System\\debug.ini\\SAPCloud.Comunicacion\\SAPCloud.Comunicacion.exe";
        public static string Url_userModel128 = string.Format("{0}{1}", baseDir, @"\Models\userModel128.png");
        public static string Url_Img_RutaUsuarios = string.Empty;

        #region "IDs"
        public static int CheckInOutId;
        public static int JusitificacionMotivoId;
        public static int ScheduleId;
        public static int JustificacionId;
        public static string TimeId;
        public static int DeviceId;
        public static int UsuarioId;
        public static int CompanyId;
        public static int AreaId;
        public static int UserTimeId;
        public static int IdPerfil;
        public static string str_UsuarioIds;
        public static int HolidayId;
        public static int UserContractId;
        public static int BOId;
        public static string UserDeviceID;

        #endregion
        #region "DataTables"

        public static DataTable Tabla = new DataTable();
        public static DataTable dt_UserIds = new DataTable();
        public static DataTable dt_UserDeviceIds = new DataTable();
        public static DataTable dt_General = new DataTable();
        public static DataTable dt_Resumen = new DataTable();
        public static DataTable dt_Marcaciones = new DataTable();
        public static DataTable dt_Tardanza = new DataTable();
        public static DataTable dt_Justificaciones = new DataTable();
        public static DataTable dt_Faltas = new DataTable();
        public static DataTable dt_SalidasTemprano = new DataTable();
        public static DataTable dt_Company = new DataTable();
        public static DataTable dt_Company_Report = new DataTable();
        #endregion
        #region "Login"        
        public static DataTable Login_dt;        
        public static bool bDBverify;
        public static string str_Permisos;
        public static int int_UserSys;
        public static int UsuarioId_Calendario;
        public static int UserTimeId_Calendario;
        public static int Login_CompanyId;
        public static int Login_UserAccountId;
        public static string Login_CompanyCode;
        public static string Login_CompanyName;
        public static string Login_CompanyRUC;
        public static byte[] Login_CompanyLogo;        
        public static int Login_BOId;
        public static string Login_BOName;
        public static int Login_AreaId;
        public static string Login_AreaName;
        public static int Login_IdPerfil;
        public static string Login_UserTypeName;
        public static DateTime Login_CurrentDateTime;
        public static int Login_Year;
        public static int Login_Month;
        public static string Login_Month_Letters;
        public static string Login_UDeviceId;
        public static int Login_Day;
        public static string Login_UserName;
        public static string Login_UserPassword;
        #endregion
        #region "Instalador"
        public static int iChoose;
            public static string ConnectionString;
            public static string UseMode;
            public static string UserName;
            public static string Password;
            public static string InitialCatalog;
            public static string DataSource;        
        #endregion
        public static int Main_DeviceId;

        

        public static int[] users;
        public static int int_users;
        public static int int_confirmed;        
        public static int int_Mantenimiento;        
        public static DateTime startDate;
        public static DateTime endDate;

        public static string strStartDate;
        public static string strEndDate;

        public static string destinationFileSQL;
        public static string str_ScheduleCode;
        public static string strHorario;

        #region "Format_dt_UserIds"
        
        public static void Format_dt_UserIds()
        {
            if (dt_UserIds.Columns.Contains("UsuarioId")==false) 
            {
                dt_UserIds.Columns.Add("UsuarioId");
            }            
        }
        #endregion

        #region "Format_dt_UserDeviceIds"
        public static void Format_dt_UserDeviceIds()
      {          
          if (dt_UserDeviceIds.Columns.Contains("UDeviceId") == false)
          {
              dt_UserDeviceIds.Columns.Add("UDeviceId");
          }  
      }
        #endregion

       

        #region "Imagen_A_Bytes"
        public static Byte[] Imagen_A_Bytes(String ruta)
        {
            FileStream foto = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Byte[] arreglo = new Byte[foto.Length];
            BinaryReader reader = new BinaryReader(foto);
            arreglo = reader.ReadBytes(Convert.ToInt32(foto.Length));
            return arreglo;
        }
        #endregion

        

       
    }
}