﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Common
{
    public class Encryptions
    {

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
                UTF8Encoding.UTF8.GetBytes(Constants.Key));

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
            catch (Exception)
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
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Constants.Key));

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
            catch (Exception)
            {
                
            }
            return Rpta;
        }

        #endregion

    }
}
