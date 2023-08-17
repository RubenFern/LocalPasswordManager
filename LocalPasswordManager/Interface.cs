using System;
using System.Numerics;
using System.Text;
using Library;
using Microsoft.VisualBasic.FileIO;

namespace LocalPasswordManager
{
    public class Interface
    {
        private const string SITE = "SITE";
        private const string USERNAME = "USERNAME";
        private const string EMAIL = "EMAIL";
        private const string PASSWORD = "PASSWORD";

        private List<Site> sites;
        private int numCharacters;
        private int maxCharactersSite;
        private int maxCharactersUsername;
        private int maxCharactersEmail;
        private int maxCharactersPassword;
        private int minCharactersSite;
        private int minCharactersUsername;
        private int minCharactersEmail;
        private int minCharactersPassword;

        public Interface() 
        {
            sites = Util.GetPasswords();

            maxCharactersSite = GetMaxLengthCharacterSite();
            maxCharactersUsername = GetMaxLengthCharacterUsername();
            maxCharactersEmail = GetMaxLengthCharacterEmail();
            maxCharactersPassword = GetMaxLengthCharacterPassword();

            minCharactersSite = GetMinLengthCharacterSite();
            minCharactersUsername = GetMinLengthCharacterUsername();
            minCharactersEmail = GetMinLengthCharacterEmail();
            minCharactersPassword = GetMinLengthCharacterPassword();

            numCharacters = CalculateCharacterNumber();
        }

        public void LoadInterface()
        {
            PrintTitle();
            PrintHeader();
            PrintBody();
            PrintSeparator();

            int option = 0;

            do
            {
                PrintMenu();

                option = ReadOption();
            }
            while (option != 5);

            
        }

        private void PrintTitle()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\tLOCAL PASSWORD MANAGER");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintHeader()
        {
            PrintBar();

            Console.WriteLine(String.Format("{0}{1}{2}{3}{4}", 
                                            PrintInicialColumn(), 
                                            PrintColumn(maxCharactersSite, SITE),
                                            PrintColumn(maxCharactersUsername, USERNAME),
                                            PrintColumn(maxCharactersEmail, EMAIL),
                                            PrintColumn(maxCharactersPassword, PASSWORD)));

            PrintBar();
        }

        private void PrintBar()
        {
            Console.Write("+");

            for (int i = 0; i < numCharacters; i++)
                Console.Write("-");

            Console.Write("+");

            Console.WriteLine();
        }

        private void PrintBody()
        {
            foreach (Site s in sites)
                PrintLine(s.SiteName, s.UserName, s.Email, Encoding.UTF8.GetString(s.Password));
        }

        private void PrintLine(string site, string username, string email, string password)
        {
            Console.WriteLine(String.Format("{0}{1}{2}{3}{4}", 
                                            PrintInicialColumn(), 
                                            PrintColumn(maxCharactersSite, site),
                                            PrintColumn(maxCharactersUsername, username),
                                            PrintColumn(maxCharactersEmail, email),
                                            PrintColumn(maxCharactersPassword, password)));

            PrintBar();
        }

        private string PrintInicialColumn()
        {
            return "|";
        }

        private string PrintColumn(int maxCharacters, string str)
        {
            if (str.Length < maxCharacters)
                return $"{Tab()}{str}{Tab(maxCharacters - str.Length)}|";

            return $"{Tab()}{str}{Tab()}|";
        }

        private void PrintSeparator()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < numCharacters + 2; i++)
                Console.Write("=");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Introduzca el número de operación que desee:");
            Console.WriteLine("1 - Listar todas las contraseñas");
            Console.WriteLine("2 - Añadir contraseña");
            Console.WriteLine("3 - Mostrar contraseña");
            Console.WriteLine("4 - Borrar contraseña");
            Console.WriteLine("5 - Salir");

            Console.ForegroundColor = ConsoleColor.White;
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
                    PrintMenu();

                    option = ReadOption();
                }
            }
            catch (OverflowException)
            {
                Util.WarningMessage("El número debe de estar entre las opciones 1 y 5.");
                PrintMenu();

                option = ReadOption();
            }
            catch (FormatException)
            {
                Util.WarningMessage("El valor introducido no es un número entero.");
                PrintMenu();

                option = ReadOption();
            }

            return option;
        }

        private string Tab()
        {
            return "".PadRight(8, ' ');
        }

        private string Tab(int addition)
        {
            return "".PadRight(8 + addition, ' ');
        }

        private int CalculateCharacterNumber()
        {
            int numPropsClassSite = typeof(Site).GetProperties().Count();

            int additionalSpace = Math.Min(maxCharactersSite - minCharactersSite, 
                                           Math.Min(maxCharactersUsername - minCharactersUsername,
                                           Math.Min(maxCharactersEmail - minCharactersEmail, maxCharactersPassword - minCharactersPassword)));

            return maxCharactersSite + 16 + maxCharactersUsername + 16 + maxCharactersEmail + 16 + maxCharactersPassword + 16 + numPropsClassSite - additionalSpace;
        }

        private int GetMaxLengthCharacterSite()
        {
            return Math.Max(SITE.Length, sites.Max(s => s.SiteName.Length));
        }

        private int GetMinLengthCharacterSite()
        {
            return Math.Min(SITE.Length, sites.Min(s => s.SiteName.Length));
        }

        private int GetMaxLengthCharacterUsername()
        {
            return Math.Max(USERNAME.Length, sites.Max(s => s.UserName.Length));
        }

        private int GetMinLengthCharacterUsername()
        {
            return Math.Min(USERNAME.Length, sites.Min(s => s.UserName.Length));
        }

        private int GetMaxLengthCharacterEmail()
        { 
            return Math.Max(EMAIL.Length, sites.Max(s => s.Email.Length));
        }

        private int GetMinLengthCharacterEmail()
        {
            return Math.Min(EMAIL.Length, sites.Min(s => s.Email.Length));
        }

        private int GetMaxLengthCharacterPassword()
        {
            return Math.Max(PASSWORD.Length, sites.Max(s => s.Password.Length));
        }

        private int GetMinLengthCharacterPassword()
        {
            return Math.Min(PASSWORD.Length, sites.Min(s => s.Password.Length));
        }

    }
}
