using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexApp
{
    class PhoneBookHandler
    {
        public string[] Accounts { get; set; }
        public string FullFileName { get; set; }
        public PhoneBookHandler(string phoneBookFile)
        {
            FullFileName = phoneBookFile;
            if (CheckDataFile(FullFileName))
                Accounts = GetDataArray(ReadDataFile(FullFileName));
            else
                Console.WriteLine($"File {FullFileName} not exist!");
        }

        public void ProcessUserInput(string input)
        {
            switch (input)
            {
                case "a":
                    {
                        ViewAllContact();
                        break;
                    }
                case "u":
                    {
                        string newAccount = ValidContact();
                        if (newAccount != null)
                        {
                            UpdateDataFile(newAccount, FullFileName);
                            Accounts = GetDataArray(ReadDataFile(FullFileName));
                        }
                        break;
                    }
                case "f":
                    {
                        SearchData("f");
                        break;
                    }
                case "l":
                    {
                        SearchData("l");
                        break;
                    }
                case "p":
                    {
                        SearchData("p");
                        break;
                    }
            }
            Console.WriteLine("For continue press any key...");
            Console.ReadKey();
        }
        private void SearchData(string kindOfSearching)
        {
            Console.WriteLine("Enter patern for searching");
            Console.WriteLine("For searching by phonenumber, enter the first three digits of the number");
            string userInput = Console.ReadLine();
            string patern = string.Empty;
            switch (kindOfSearching)
            {
                case "l":
                    {
                        patern = @"(^)" + userInput + @"(\w*\s)";
                        break;
                    }
                case "f":
                    {
                        patern = @"(\s)" + userInput + @"(\w*\s)";
                        break;
                    }
                case "p":
                    {
                        patern = @"(\s)" + userInput + @"-\d{3}-\d{3}";
                        break;
                    }
            }
            for (int i = 0; i < Accounts.Length; i++)
            {
                if (Regex.IsMatch(Accounts[i], patern, RegexOptions.IgnoreCase))
                    Console.WriteLine(Accounts[i]);
            }
        }

        public void PrintManual()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("To work with the program, enter the following characters");
            sb.AppendLine("To view all phonebook, enter:     'A'");
            sb.AppendLine("To add data, enter:               'U'");
            sb.AppendLine("To search by first name, enter:   'F'");
            sb.AppendLine("To search by last name, enter:    'L'");
            sb.AppendLine("To search by phone number, enter: 'P'");
            sb.AppendLine("To exit the program, enter:       'X'");
            Console.Clear();
            Console.WriteLine(sb);
        }

        private void ViewAllContact()
        {
            foreach (var item in Accounts)
            {
                Console.WriteLine(item);
            }
        }

        private string ValidContact()
        {
            Console.WriteLine("Enter LastName, FirstName and PhoneNumber(in format XXX-XXX-XXX) divided by whitespace");
            var newContact = Console.ReadLine();
            if (CheckEnterData(newContact))
                return newContact;
            else
                Console.WriteLine("Invalid input");
            return null;
        }

        private bool CheckEnterData(string newContact)
        {
            string checkPatern = @"^\w*\s\w*\s\d{3}-\d{3}-\d{3}";
            return Regex.IsMatch(newContact, checkPatern);
        }

        private void UpdateDataFile(string newData, string fullFileName)
        {
            StringBuilder fileContent = ReadDataFile(fullFileName);
            fileContent.AppendLine(newData);
            var orderedArray = fileContent.ToString().Split(new string[] {"\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).OrderBy(a => a).ToArray();
            File.Delete(fullFileName);
            using (StreamWriter writer = new StreamWriter(fullFileName))
            {
                foreach (var item in orderedArray)
                {
                    writer.WriteLine(item);
                }
                writer.Close();
            }
        }

        private StringBuilder ReadDataFile(string phoneBookFile)
        {
            StringBuilder content = null;
            using (StreamReader reader = new StreamReader(phoneBookFile))
            {
                content = new StringBuilder(reader.ReadToEnd());
            }
            return content;
        }

        private string[] GetDataArray(StringBuilder content)
        {
            return content.ToString().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool CheckDataFile(string fullFileName)
        {
            return File.Exists(fullFileName);
        }
    }
}
