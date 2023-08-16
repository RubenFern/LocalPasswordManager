using Library;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LocalPaswordManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key = "abcdefghijklmnop";

            string site = "Amazon";
            string email = "rr@rr.com";
            string password = "contrasena";

            PasswordEncrypter e = new PasswordEncrypter(key);

            Site amazon = new Site()
            {
                SiteName = site,
                Email = email,
                Password = e.Encrypt(password)
        };

            string json = JsonSerializer.Serialize(amazon);

            Console.WriteLine(json);
        }
    }
}