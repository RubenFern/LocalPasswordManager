using Library;
using System;

namespace LocalPasswordManager
{
    public class OptionManager
    {
        private Interface _interface;

        enum Operation
        {
            READ,
            WRITE,
            REMOVE
        }

        public OptionManager() 
        { 
            _interface = new Interface();

            _interface.PrintTitle();
        }

        public void Execute()
        {
            int option;

            do
            {
                _interface.PrintMenu();

                option = ReadOption();

                ExecuteOption(option);
            }
            while (option != 5);
        }

        public void ExecuteOption(int option)
        {
            switch (option)
            {
                case 1:
                    ShowPasswords();

                    break;
                case 2:
                    AddPassword();
                    
                    break;
                case 3:
                    ShowPassword();
                    
                    break;
                case 4:
                    RemovePassword();

                    break;
                case 5:
                    Console.WriteLine();
                    Console.WriteLine("Hasta pronto!");

                    break;
            }
        }

        private int ReadOption()
        {
            int option;
            string input = Console.ReadLine() ?? "";

            try
            {
                option = int.Parse(input);

                while (option < 1 || option > 5)
                {
                    Util.WarningMessage("Opción no reconocida");
                    _interface.PrintMenu();

                    option = ReadOption();
                }
            }
            catch (OverflowException)
            {
                Util.WarningMessage("El número debe de estar entre las opciones 1 y 5.");
                _interface.PrintMenu();

                option = ReadOption();
            }
            catch (FormatException)
            {
                Util.WarningMessage("El valor introducido no es un número entero.");
                _interface.PrintMenu();

                option = ReadOption();
            }

            return option;
        }

        private void ShowPasswords()
        {
            _interface.UpdatePasswords();

            if (Util.GetPasswords().Count == 0)
            {
                Util.InformativeMessage("No hay ninguna contraseña guardada.");
                return;
            }

            _interface.PrintHeader();
            _interface.PrintBody();
            _interface.PrintSeparator();
        }

        private void AddPassword()
        {
            string siteName = GetSiteName();
            string username = GetUsername();
            string email = GetEmail();
            string password = GetPassword();
            string key = GetKey(Operation.WRITE);
            
            PasswordEncrypter encrypter = new PasswordEncrypter(key);

            Site site = new Site(Util.GetPasswords().Count() + 1, siteName, username, email, encrypter.Encrypt(password));

            Util.SaveSite(site);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contraseña añadida!!");
            Console.ForegroundColor = ConsoleColor.White;

            ShowPasswords();
        }

        private void ShowPassword()
        {
            List<Site> sites = Util.GetPasswords();

            int id = GetId(sites, Operation.READ);

            Site site = sites.Find(s => s.Id == id) ?? new Site();

            if (site is null || site.Id == -1)
                return;

            string key = GetKey(Operation.READ);

            PasswordEncrypter encrypter = new PasswordEncrypter(key);

            _interface.PrintSite(site, encrypter.Decrypt(site.Password));
        }

        private void RemovePassword()
        {
            List<Site> sites = Util.GetPasswords();

            int id = GetId(sites, Operation.REMOVE);

            Site site = sites.Find(s => s.Id == id) ?? new Site();

            if (site is null || site.Id == -1)
                return;

            if (Util.RemoveSite(site))
                Util.InformativeMessage("Contraseña eliminada!!");
            else
                Util.WarningMessage("No ha sido posible eliminar la contraseña.");
        }

        private int GetId(List<Site> sites, Operation operation)
        {
            int id;

            if (operation == Operation.REMOVE)
                Console.Write("Introduce el Id de la contraseña que quieres borrar:\t");
            else
                Console.Write("Introduce el Id de la contraseña que quieres ver:\t");

            string input = Console.ReadLine() ?? "";

            try
            {
                id = int.Parse(input);

                while (!sites.Exists(s => s.Id == id))
                {
                    Util.WarningMessage("El Id no coincide con ninguna contraseña.");
                    _interface.PrintMenu();

                    id = GetId(sites, operation);
                }
            }
            catch (OverflowException)
            {
                Util.WarningMessage("El Id es demasiado grande.");
                _interface.PrintMenu();

                id = GetId(sites, operation);
            }
            catch (FormatException)
            {
                Util.WarningMessage("El Id introducido no es un número entero.");
                _interface.PrintMenu();

                id = GetId(sites, operation);
            }

            return id;
        }

        private string GetSiteName()
        {
            Console.Write("Nombre del sitio:\t");
            string site = Console.ReadLine() ?? "";

            while (string.IsNullOrEmpty(site))
            {
                Util.WarningMessage("Debes indicar un nombre del sitio:\t");
                site = Console.ReadLine() ?? "";
            }

            return site;
        }

        private string GetUsername()
        {
            Console.Write("Nombre de usuario:\t");
            string username = Console.ReadLine() ?? "";

            return username;
        }

        private string GetEmail()
        {
            Console.Write("Dirección de correo electrónico:\t");
            string email = Console.ReadLine() ?? "";

            return email;
        }

        private string GetPassword()
        {
            Console.Write("Contraseña:\t");
            string password = Console.ReadLine() ?? "";

            while (string.IsNullOrEmpty(password))
            {
                Util.WarningMessage("Debes indicar una contraseña:\t");
                password = Console.ReadLine() ?? "";
            }

            return password;
        }

        private string GetKey(Operation operation) 
        {
            if (operation == Operation.WRITE)
            {
                Util.InformativeMessage("Debes indicar una clave de 16 dígitos asociada al cifrado de la contraseña, ");
                Util.InformativeMessage("CUIDADO debes recordar esta clave para poder descifrar luego la contraseña");
            }
            else
                Util.InformativeMessage("Debes indicar la clave de 16 dígitos que usaste al cifrar esta contraseña.");
  
            Console.Write("Clave:\t");
            string key = Console.ReadLine() ?? "";

            while (string.IsNullOrEmpty(key) || key.Length != 16)
            {
                if (operation == Operation.WRITE)
                    Util.WarningMessage("Debes indicar una clave de 16 dígitos asociada al cifrado de la contraseña");
                else
                    Util.WarningMessage("Debes indicar la clave de 16 dígitos que usaste al cifrar esta contraseña.");

                Console.Write("Clave:\t");
                key = Console.ReadLine() ?? "";
            }

            return key;
        }
    }
}
