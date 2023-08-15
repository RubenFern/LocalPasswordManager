using Library;
using System;
using System.Text;

namespace LocalPaswordManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password = "contrasena";
            string key = "";

            for (int i = 0; i < 16; i++)
                key += "b";

            PasswordEncrypter e = new PasswordEncrypter(key);

            byte[] cifrado = e.Encrypt(password);

            Console.WriteLine(password);
            Console.WriteLine(e.ShowPasswordEncrypted(cifrado));
            Console.WriteLine(e.Decrypt(cifrado));

            byte[] bytes = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
            Console.WriteLine(e.Decrypt(bytes));

            Console.WriteLine(e.Decrypt(null));
        }
    }
}