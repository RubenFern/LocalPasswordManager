using Library;

namespace LocalPasswordManager
{
    public class OptionManager
    {
        private Interface _interface;

        public OptionManager() 
        { 
            _interface = new Interface();

            _interface.PrintTitle();
        }

        public void Execute()
        {
            int option = 0;

            do
            {
                _interface.PrintMenu();

                option = ReadOption();

                ExecuteOption(option);
            }
            while (option != 5);

            Console.WriteLine();
            Console.WriteLine("Hasta pronto!");
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
                    
                    break;
                case 4:
                    
                    break;
                case 5:
                    
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
            string key = GetKey();
            
            PasswordEncrypter encrypter = new PasswordEncrypter(key);

            Site site = new Site(Util.GetPasswords().Count() + 1, siteName, username, email, encrypter.Encrypt(password));

            Util.SaveSite(site);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contraseña añadida!!");
            Console.ForegroundColor = ConsoleColor.White;

            ShowPasswords();
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

        private string GetKey() 
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.Write("Debes indicar una clave de 16 dígitos asociada al cifrado de la contraseña, ");
            Console.WriteLine("CUIDADO debes recordar esta clave para poder descifrar luego la contraseña");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Clave:\t");
            string key = Console.ReadLine() ?? "";

            while (string.IsNullOrEmpty(key) || key.Length != 16)
            {
                Util.WarningMessage("Debes indicar una clave de 16 dígitos asociada al cifrado de la contraseña");
                Console.Write("Clave:\t");
                key = Console.ReadLine() ?? "";
            }

            return key;
        }
    }
}
