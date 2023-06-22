﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static void RenameFiles(string directoryPath, string filePattern)
        {
            // Retrieve the list of files in the specified directory
            string[] fileNames = Directory.GetFiles(directoryPath);

            Console.WriteLine("All Files:");
            // Display all the file names before renaming
            foreach (string fileName in fileNames)
            {
                Console.WriteLine(fileName);
            }

            Console.WriteLine("Renamed Files:");
            // Rename files
            foreach (string fileName in fileNames)
            {
                string modifiedFileName = ExtractPatternAndModifyFileName(fileName, filePattern);
                string newPath = Path.Combine(directoryPath, modifiedFileName);

                // Check if the source and destination paths are the same
                if (!string.Equals(fileName, newPath, StringComparison.OrdinalIgnoreCase))
                {   
                    if (File.Exists(newPath))
                    {
                        throw new IOException($"Destination file already exists: {newPath}");
                    }
                    File.Move(fileName, newPath);
                    Console.WriteLine(newPath);
                }
            }
        }


        private static string ExtractPatternAndModifyFileName(string fileName, string filePattern)
        {
            string pattern = Regex.Escape(filePattern);
            pattern = pattern.Replace("\\*", "(.*?)");
            pattern = pattern.Replace("\\?", ".");
            pattern = pattern.Replace("**", ".*?");

            Match match = Regex.Match(fileName, pattern);

            string capturedValue = fileName.Substring(match.Index + match.Length);
            return capturedValue;
        }








    }
}
