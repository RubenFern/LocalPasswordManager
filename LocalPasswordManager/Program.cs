using Library;
using System;
using System.Text;

namespace LocalPaswordManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string site = "Amazon";
            string email = "rr@rr.com";
            string password = "contrasena";
            string key = "abcdefghijklmnop";

            PasswordEncrypter e = new PasswordEncrypter(key);

            byte[] passCifrada = e.Encrypt(password);

            Site amazon = new Site(site, email, e.PasswordEncryptedToString(passCifrada));

            Console.WriteLine(amazon);
        }
    }
}