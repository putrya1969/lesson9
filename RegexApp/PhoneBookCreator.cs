using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexApp
{
    class PhoneBookCreator
    {
        string firstNamesFile = Path.Combine(Environment.CurrentDirectory, "FirstNames.txt");
        string lastNamesFile = Path.Combine(Environment.CurrentDirectory, "LastNames.txt");
        string phoneBookFile = Path.Combine(Environment.CurrentDirectory, "PhoneBook.txt");
        public string[] LastNames { get; set; }
        public string[] FirstNames { get; set; }
        public string[] Accounts { get; } = null;
        public Random RandomNumber { get; set; }
        public PhoneBookCreator(int accountCount)
        {
            FirstNames = new FileHandler(firstNamesFile).Content;
            LastNames = new FileHandler(lastNamesFile).Content;
            string[] accounts = new string [accountCount];
            RandomNumber = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < accountCount; i++)
            {
                accounts[i] = CreateAccount(RandomNumber);
            }
            Accounts = accounts;
            SaveDataToFile(phoneBookFile);
        }

        public string CreateAccount(Random random)
        {
            return $"{GetLastName(random.Next(0, LastNames.Length - 1))} {GetFirstName(random.Next(0, FirstNames.Length - 1))} {CreatePhoneNumber(random)}";
        }

        private string GetFirstName(int index)
        {
            return FirstNames[index];
        }

        private string GetLastName(int index)
        {
            return LastNames[index];
        }

        private string CreatePhoneNumber(Random random)
        {
            int seed = DateTime.Now.Millisecond;
            return $"{random.Next(100,999)}" +
                $"-{random.Next(100, 999)}" +
                $"-{random.Next(100, 999)}";
        }

        public void SaveDataToFile(string phoneBookFile)
        {
            using (StreamWriter writer = new StreamWriter(phoneBookFile))
            {
                StringBuilder builder = new StringBuilder();
                var orderedAcc = Accounts.OrderBy(a => a).ToArray();
                foreach (var item in orderedAcc)
                {
                    builder.AppendLine(item);
                }
                writer.Write(builder);
                writer.Close();
            }
        }
    }
}
