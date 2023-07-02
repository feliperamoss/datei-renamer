using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRenamer
{
    internal class CommandLineExecutor
    {
        public static void Execute(string[] args)
        {
            if (args.Length > 1 && args[0] == "renamer")
            {
                string directoryPath = Environment.CurrentDirectory;
                string sourceFilePattern = args[1];
                string destinationFilePattern = args.Length > 2 ? args[2] : "";
                FileHelper.RenameFiles(directoryPath, sourceFilePattern, destinationFilePattern);
            }
            else
            {
                Console.WriteLine("Invalid command. Please use the 'renamer' command with a file pattern.");
            }
        }
    }
}
