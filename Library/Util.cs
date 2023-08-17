using System.Text.Json;
using System.Text.RegularExpressions;

namespace Library
{
    public class Util
    {
        private const string FILE_NAME = "passwords.json";

        public static void SuccesfullMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void InformativeMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WarningMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("Debe indicar un Email");

            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-]+\.)+[a-zA-Z]{2,6}$");

            return regex.IsMatch(email);
        }

        public static bool SaveSite(Site site)
        {
            if (!File.Exists(FILE_NAME))
                throw new ArgumentException("El fichero no existe.");

            if (site is null)
                throw new ArgumentNullException("El sitio no puede ser nulo.");

            // Leo todas las contraseñas almacenadas
            string passwordsJson = File.ReadAllText(FILE_NAME);

            // Fichero de contraseñas vacío
            if (string.IsNullOrEmpty(passwordsJson))
            {
                File.WriteAllText(FILE_NAME, JsonSerializer.Serialize( new List<Site> { site } ));

                return true;
            }

            // Guardo en una lista las contraseñas ya almacenadas
            List<Site> sites = PasswordsToList(passwordsJson).ToList();

            sites.Add(site);

#if DEBUG
            /*foreach (Site s in sites)
                Console.WriteLine(s);*/
#endif

            File.WriteAllText(FILE_NAME, JsonSerializer.Serialize(sites));

            return true;
        }

        public static bool RemoveSite(Site site)
        {
            if (!File.Exists(FILE_NAME))
                throw new ArgumentException("El fichero no existe.");

            if (site is null)
                throw new ArgumentNullException("El sitio no puede ser nulo.");

            // Leo todas las contraseñas almacenadas
            string passwordsJson = File.ReadAllText(FILE_NAME);

            // Fichero de contraseñas vacío
            if (string.IsNullOrEmpty(passwordsJson))
                return false;

            // ELimino la contraseña indicada
            List<Site> sites = PasswordsToList(passwordsJson).Where(s => s.Id != site.Id).ToList();

#if DEBUG
            /*foreach (Site s in sites)
                Console.WriteLine(s);*/
#endif

            File.WriteAllText(FILE_NAME, JsonSerializer.Serialize(sites));

            return true;
        }

        public static List<Site> GetPasswords()
        {
            string passwordsJson = File.ReadAllText(FILE_NAME);

            if (string.IsNullOrEmpty(passwordsJson))
                return new List<Site>();

            return PasswordsToList(passwordsJson).ToList();
        }

        private static IEnumerable<Site> PasswordsToList(string dataJson)
        {
            var passwords = JsonSerializer.Deserialize<List<Site>>(dataJson);

            if (passwords is null)
                return new List<Site>();

            return passwords;
        }
    }
}
