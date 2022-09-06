using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sha256WithRsa
{
    class ProgramSHA256
    {
        static void Main(String[] args)
        {
            Console.Write("Escribe el texto a hashear: ");
            string input = Console.ReadLine();
            String inputHash = ToSHA256(input);
            Console.WriteLine( $"Texto hasheado: {inputHash}");

            Console.ReadLine();

            Console.Write("Escribe el texto a verificar: ");
            string inputVerification = Console.ReadLine();
            String inputVerificationHash = ToSHA256(inputVerification);
            Console.ReadLine();

            if (inputVerificationHash == inputHash)
            {
                Console.WriteLine("El texto coincide");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("NO coincide");
                Console.ReadLine();

            }
        }

        public static string ToSHA256(string s)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
