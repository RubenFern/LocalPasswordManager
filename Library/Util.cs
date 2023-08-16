using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Library
{
    public class Util
    {
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

        public static bool SaveSite(string fileName, Site site)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("El nombre del fichero no puede ser nulo.");

            if (!File.Exists(fileName))
                throw new ArgumentException("El fichero no existe.");

            if (site is null)
                throw new ArgumentNullException("El sitio no puede ser nulo.");

            // Leo todas las contraseñas almacenadas
            string passwordsJson = File.ReadAllText(fileName);

            // Fichero de contraseñas vacío
            if (string.IsNullOrEmpty(passwordsJson))
            {
                File.WriteAllText(fileName, JsonSerializer.Serialize( new List<Site> { site } ));

                return true;
            }

            // Guardo en una lista las contraseñas ya almacenadas
            List<Site> sites = PasswordsToList(passwordsJson, site);

            sites.Add(site);

#if DEBUG
            foreach (Site s in sites)
                Console.WriteLine(s);
#endif

            File.WriteAllText(fileName, JsonSerializer.Serialize(sites));

            return true;
        }

        private static List<T> PasswordsToList<T>(string dataJson, T site)
        {
            var passwords = JsonSerializer.Deserialize<List<T>>(dataJson);

            if (passwords is null)
                return new List<T> { site };

            return passwords;
        }
    }
}
