using Library;

namespace LocalPasswordManager
{
    public class Interface
    {
        private const string ID = "ID";
        private const string SITE = "SITE";
        private const string USERNAME = "USERNAME";
        private const string EMAIL = "EMAIL";
        private const string PASSWORD = "PASSWORD";

        private List<Site> sites;
        private int numCharacters;
        private int maxCharactersId;
        private int maxCharactersSite;
        private int maxCharactersUsername;
        private int maxCharactersEmail;
        private int maxCharactersPassword;
        private int minCharactersId;
        private int minCharactersSite;
        private int minCharactersUsername;
        private int minCharactersEmail;
        private int minCharactersPassword;

        public Interface() 
        {
            sites = Util.GetPasswords();

            UpdatePasswords();
        }

        public void UpdatePasswords()
        {
            sites = Util.GetPasswords();

            if (sites is null || sites.Count == 0)
                return;

            maxCharactersId = 4;
            maxCharactersSite = GetMaxLengthCharacterSite();
            maxCharactersUsername = GetMaxLengthCharacterUsername();
            maxCharactersEmail = GetMaxLengthCharacterEmail();
            maxCharactersPassword = GetMaxLengthCharacterPassword();

            minCharactersId = 3;
            minCharactersSite = GetMinLengthCharacterSite();
            minCharactersUsername = GetMinLengthCharacterUsername();
            minCharactersEmail = GetMinLengthCharacterEmail();
            minCharactersPassword = GetMinLengthCharacterPassword();

            numCharacters = CalculateCharacterNumber();
        }

        public void PrintTitle()
        {
            Util.InformativeMessage("\t\t\t\t\tLOCAL PASSWORD MANAGER");
        }

        public void PrintHeader()
        {
            PrintBar();

            Console.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}", 
                                            PrintInicialColumn(), 
                                            PrintColumn(maxCharactersId, ID),
                                            PrintColumn(maxCharactersSite, SITE),
                                            PrintColumn(maxCharactersUsername, USERNAME),
                                            PrintColumn(maxCharactersEmail, EMAIL),
                                            PrintColumn(maxCharactersPassword, PASSWORD)));

            PrintBar();
        }

        public void PrintBar()
        {
            Console.Write("+");

            for (int i = 0; i < numCharacters; i++)
                Console.Write("-");

            Console.Write("+");

            Console.WriteLine();
        }

        public void PrintBody()
        {
            foreach (Site s in sites)
                PrintLine(s.Id, s.SiteName, s.UserName, s.Email, s.Password);
        }

        private void PrintLine(int id, string site, string username, string email, byte[] password)
        {
            Console.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}", 
                                            PrintInicialColumn(),
                                            PrintColumn(maxCharactersId, id.ToString()),
                                            PrintColumn(maxCharactersSite, site),
                                            PrintColumn(maxCharactersUsername, username),
                                            PrintColumn(maxCharactersEmail, email),
                                            PrintColumn(maxCharactersPassword, Convert.ToBase64String(password))));

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

        public void PrintSeparator()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            for (int i = 0; i < numCharacters + 2; i++)
                Console.Write("=");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public void PrintMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("Introduzca el número de operación que desee:");
            Console.WriteLine("1 - Listar todas las contraseñas");
            Console.WriteLine("2 - Añadir contraseña");
            Console.WriteLine("3 - Mostrar contraseña");
            Console.WriteLine("4 - Borrar contraseña");
            Console.WriteLine("5 - Salir");

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintSite(Site site, string password)
        {
            if (site is null || string.IsNullOrEmpty(password))
                return;

            Util.SuccesfullMessage($"{site.SiteName}:");

            if (!site.UserName.Equals(""))
                Util.SuccesfullMessage($"{Tab()}Username: {site.UserName}");

            if (!site.Email.Equals(""))
                Util.SuccesfullMessage($"{Tab()}Email: {site.Email}");

            Util.SuccesfullMessage($"{Tab()}Password: {password}");
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

            int additionalSpace = Math.Min(maxCharactersId - minCharactersId, Math.Min(maxCharactersSite - minCharactersSite, 
                                        Math.Min(maxCharactersUsername - minCharactersUsername,
                                        Math.Min(maxCharactersEmail - minCharactersEmail, maxCharactersPassword - minCharactersPassword))));

            return maxCharactersId + 16 + maxCharactersSite + 16 + 
                   maxCharactersUsername + 16 + maxCharactersEmail + 16 + 
                   maxCharactersPassword + 16 + numPropsClassSite - additionalSpace;
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
            return Math.Max(PASSWORD.Length, sites.Max(s => Convert.ToBase64String(s.Password).Length));
        }

        private int GetMinLengthCharacterPassword()
        {
            return Math.Min(PASSWORD.Length, sites.Min(s => Convert.ToBase64String(s.Password).Length));
        }

    }
}
