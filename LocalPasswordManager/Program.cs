using Library;

namespace LocalPaswordManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key = "abcdefghijklmnop";

            string password = "contrasena";

            PasswordEncrypter e = new PasswordEncrypter(key);

            Site amazon = new Site()
            {
                Id = 1,
                SiteName = "Amazon",
                Email = "rr@rr.com",
                Password = e.Encrypt(password)
            };

            Site google = new Site()
            {
                Id = 2,
                SiteName = "Google",
                Email = "rr2@rr.com",
                Password = e.Encrypt(password)
            };

            Console.WriteLine("Salvar");
            Util.SaveSite(amazon);
            Util.SaveSite(google);

            Console.WriteLine("Borrar");
            //Util.RemoveSite(amazon);
        }
    }
}