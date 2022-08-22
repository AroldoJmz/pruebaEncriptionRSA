using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PruebaRSAEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            RsaEncryption rsa = new RsaEncryption();
            string cypher = string.Empty;

            Console.WriteLine($"Llave pública: {rsa.GetPublicKey() } \n");

            Console.WriteLine("Escribe un texto para encriptar:");
            var text = Console.ReadLine();
            if (!string.IsNullOrEmpty(text))
            {
                cypher = rsa.Encrypt(text);
                Console.WriteLine($"Texto encriptado: {cypher}");
            }
            else
            {
                Console.WriteLine("Texto no válido");
            }

            Console.WriteLine("Escribe para dessencriptar");
            Console.ReadLine();
            var decryptText = rsa.Decrypt(cypher);

            Console.WriteLine($"Texto desencriptado: {decryptText}");
            Console.ReadLine();
        }
    }

    public class RsaEncryption
    {
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public RsaEncryption()
        {
            _publicKey = csp.ExportParameters(false);
            _privateKey = csp.ExportParameters(true);

        }

        public String GetPublicKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);

            return sw.ToString();
        }

        public String Encrypt(string text)
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(_publicKey);
            var data = Encoding.Unicode.GetBytes(text);
            var cypher = csp.Encrypt(data,false);
            return Convert.ToBase64String(cypher);
        }

        public String Decrypt(string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            csp.ImportParameters(_privateKey);
            var text = csp.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(text);
        }

    }
}
