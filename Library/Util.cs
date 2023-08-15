using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public static bool isValidEmail(string email)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-]+\.)+[a-zA-Z]{2,6}$");

            return regex.IsMatch(email);
        }
    }
}
