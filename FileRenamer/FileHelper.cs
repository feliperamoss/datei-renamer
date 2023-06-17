using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileRenamer
{
    internal class FileHelper
    {
        public static IEnumerable<FileInfo> GetFilesForInterface(string path)
        {
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Directory not found.");
                return Enumerable.Empty<FileInfo>();
            }

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                return directoryInfo.GetFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return Enumerable.Empty<FileInfo>();
            }
        }

        public static void GetFilesForConsole(string path)
        {
            // Check if the specified directory exists
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Directory not found.");
                return;
            }
            try
            {
                // Get all files in the directory
                string[] files = Directory.GetFiles(path);

                // Display the file names
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
