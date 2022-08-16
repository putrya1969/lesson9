using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexApp
{
    public class FileHandler
    {
        public string FullFileName { get;}
        public string[] Content { get; set; } = null;

        public FileHandler(string fullFileName)
        {
            FullFileName = fullFileName;
            if (CheckFile(FullFileName))
                Content = GetFileContent();
            else
                Console.WriteLine($"File {fullFileName} not exist!"); 
        }
        private bool CheckFile(string fullFileName)
        {
            return File.Exists(fullFileName);
        }

        private string[] GetFileContent()
        {
            StringBuilder content = null;
            using (StreamReader reader = new StreamReader(FullFileName))
            {
                content = new StringBuilder(reader.ReadToEnd());
            }
            return content.ToString().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
