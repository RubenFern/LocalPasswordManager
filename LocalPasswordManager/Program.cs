using LocalPasswordManager;

namespace LocalPaswordManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OptionManager optionManager = new OptionManager();

            optionManager.Execute();
        }
    }
}