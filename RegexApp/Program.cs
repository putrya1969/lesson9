using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileFullPath = Path.Combine(Environment.CurrentDirectory, "PhoneBook.txt");
            var phoneBookHandler = new PhoneBookHandler(fileFullPath);
            if (phoneBookHandler.Accounts == null)
            {
                Console.WriteLine("Create phonebook? Enter 'y' or 'n'");
                if (Console.ReadLine().Contains("n"))
                {
                    CloseApp("Application closed");
                }
                int quantityAccounts = 0;
                do
                {
                    Console.WriteLine("Enter quantity accounts for phonebook");
                } while (!int.TryParse(Console.ReadLine(), out quantityAccounts));
                if (quantityAccounts <= 0)
                    CloseApp("Invalid value. Application closed.");
                var phoneBookCreator = new PhoneBookCreator(quantityAccounts);
                phoneBookCreator.SaveDataToFile(fileFullPath);
                phoneBookHandler.Accounts = phoneBookCreator.Accounts;
            }
            phoneBookHandler.PrintManual();
            while (!CheckUserInput(out string inputString))
            {
                phoneBookHandler.ProcessUserInput(inputString);
                phoneBookHandler.PrintManual();
            }
            Console.WriteLine("For continue press any key...");
            Console.ReadKey();
        }

        static void CloseApp(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
            Environment.Exit(0);
        }

        static bool CheckUserInput( out string inputString)
        {
            inputString = Console.ReadLine();
            return inputString.Contains("x");
        }
    }
}
