using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Clases
{
    public class RSA
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
        public string LLavePublica() 
        {
            
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, rsa.ExportParameters(true));
            return sw.ToString();
        }
        public string LLavePrivada()
        {
           
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, rsa.ExportParameters(false));
            return sw.ToString();
        }
        static byte[] cifrarRSA(string texto, string clavePublica)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            // La clave pública usada para cifrar el texto
            rsa.FromXmlString(clavePublica);
            // Convertimos la cadena a un array de bytes
            byte[] datos = Encoding.Default.GetBytes(texto);
            // Generamos los datos encriptados y los devolvemos
            return rsa.Encrypt(datos, false);
        }
        static string descrifrarRSA(string claves, byte[] datosCifrados)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(20);
            // Las claves usadas para cifrar el texto
            rsa.FromXmlString(claves);
            // Generamos los datos desencriptados
            byte[] datos = rsa.Decrypt(datosCifrados, false);
            // Devolvemos la cadena original
            return Encoding.Default.GetString(datos);
        }
    }
}
