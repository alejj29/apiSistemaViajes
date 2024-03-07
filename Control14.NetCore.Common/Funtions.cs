using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;


namespace Control14.NetCore.Common
{
    public class Functions
    {
        public static string Conexion;

        #region Encriptacion
        public static string DecryptKey(string clave)
        {
            string Rpta = string.Empty;
            try
            {
                byte[] keyArray;
                //convierte el texto en una secuencia de bytes
                byte[] Array_a_Descifrar =
                Convert.FromBase64String(clave);

                //se llama a las clases que tienen los algoritmos
                //de encriptación se le aplica hashing
                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 =
                new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(
                UTF8Encoding.UTF8.GetBytes(Variables.LlaveSeguridad));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes =
                new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform =
                 tdes.CreateDecryptor();

                byte[] resultArray =
                cTransform.TransformFinalBlock(Array_a_Descifrar,
                0, Array_a_Descifrar.Length);

                tdes.Clear();
                //se regresa en forma de cadena
                Rpta = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                
            }
            return Rpta;
        }

        public static string EncryptKey(string cadena)
        {
            string Rpta = string.Empty;
            try
            {
                //arreglo de bytes donde guardaremos la llave
                byte[] keyArray;
                //arreglo de bytes donde guardaremos el texto
                //que vamos a encriptar
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(cadena);

                //se utilizan las clases de encriptación
                //provistas por el Framework
                //Algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                //se guarda la llave para que se le realice
                //hashing
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Variables.LlaveSeguridad));

                hashmd5.Clear();

                //Algoritmo 3DAS
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                //se empieza con la transformación de la cadena
                ICryptoTransform cTransform =
                tdes.CreateEncryptor();

                //arreglo de bytes donde se guarda la
                //cadena cifrada
                byte[] ArrayResultado =
                cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
                0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                Rpta = Convert.ToBase64String(ArrayResultado,
                       0, ArrayResultado.Length);
            }
            catch (Exception ex)
            {
               
            }
            return Rpta;
        }

        #endregion

        public static string GetIPAddress()
        {
            string localIP = "";
            try
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        localIP = ip.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return localIP;
        }

        public static string ReadSetting(string key)
        {
            System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }

        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public static string ObtenerDireccionMac()
        {
            string SMac = string.Empty;
            // Información de las tarjetas de red
            NetworkInterface[] interfaces = null;
            // Obtener todas las interfaces de red de la PC
            interfaces = NetworkInterface.GetAllNetworkInterfaces();
            // Validar la cantidad de tarjetas de red que tiene
            if (interfaces != null && interfaces.Length > 0)
            {
                // Recorrer todas las interfaces de red
                foreach (NetworkInterface adaptador in interfaces)
                {
                    PhysicalAddress direccion = adaptador.GetPhysicalAddress();
                    SMac = direccion.ToString().ToUpper();
                    break;
                }

            }
            return SMac;
        }

     

        public static bool EnviarEmail(string Titulo, string Mensaje, string Correo = null)
        {

            try
            {
                if (Correo != null)
                {
                    Variables.CorreosAdministrativos = Correo;
                }
                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("agavidiad@gmail.com", "lologramos##2018")
                };

                MailMessage mm = new MailMessage("agavidiad@gmail.com", Variables.CorreosAdministrativos, Titulo, Mensaje)
                {
                    BodyEncoding = UTF8Encoding.UTF8,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                };
                client.Send(mm);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void SetearFechaMensual(DateTime CurrentDate, ref DateTime FirstDay, ref DateTime LastDay)
        {
            FirstDay = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDay = new DateTime(CurrentDate.Year, CurrentDate.Month, FirstDay.AddMonths(1).AddDays(-1).Day);
        }

        public static string RetornarValorAppSetting(string Llave)
        {
            return ConfigurationManager.AppSettings[Llave];
        }

        public static string RetornarFechaExcepcion(int Tipo, string StartDate, string EndDate, string HoraInicio, string HoraFin)
        {
            string FechaExcepcion = string.Empty;
            switch (Tipo)
            {
                case 1:
                    FechaExcepcion = $"Desde {StartDate} hasta {EndDate}";
                    break;
                case 2:
                    FechaExcepcion = $"{StartDate} de {HoraInicio} a { HoraFin}";
                    break;
                case 3:
                    FechaExcepcion = $"{StartDate} {HoraInicio}";
                    break;
            }
            return FechaExcepcion;
        }

        public static bool PingTheDevice(string ipAdd)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ipAdd);

                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted. 
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(ipAddress, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}