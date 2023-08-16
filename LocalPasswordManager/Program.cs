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
                SiteName = "Amazon",
                Email = "rr@rr.com",
                Password = e.Encrypt(password)
            };

            Site google = new Site()
            {
                SiteName = "Google",
                Email = "rr2@rr.com",
                Password = e.Encrypt(password)
            };

            Util.SaveSite("passwords.json", amazon);
            Util.SaveSite("passwords.json", google);
        }
    }
}