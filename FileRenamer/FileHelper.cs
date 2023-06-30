    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
using static System.Net.Mime.MediaTypeNames;

    namespace FileRenamer
    {
    public class FileHelper
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

        public static void RenameFiles(string directoryPath, string sourceFilePattern, string destinationFilePattern = "")
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
                string modifiedFileName = ExtractPatternAndModifyFileName(fileName, sourceFilePattern, destinationFilePattern);
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

        private static string ExtractPatternAndModifyFileName(string fileName, string sourceFilePattern, string destinationFilePattern)
        {
            string pattern = Regex.Escape(sourceFilePattern);
            pattern = pattern.Replace("\\*", "(.*?)");
            pattern = pattern.Replace("\\?", ".");
            pattern = pattern.Replace("**", ".*?");

            Match match = Regex.Match(fileName, pattern);

            string replacedFirstPattern = sourceFilePattern.Replace("*", "");
            string replacedSecondPattern = destinationFilePattern.Replace("*", "");

            string fname = Path.GetFileNameWithoutExtension(fileName);

            //check if param starts with a number
            bool startsWithNumber = Regex.IsMatch(destinationFilePattern, @"^\d");
            bool SourceStartsWithNumber = Regex.IsMatch(sourceFilePattern, @"^\d");

            if (!string.IsNullOrEmpty(destinationFilePattern))
            {
                if (sourceFilePattern.StartsWith("*") && destinationFilePattern.StartsWith("*"))
                {
                    if (Path.GetExtension(fileName).Equals("." + replacedFirstPattern, StringComparison.OrdinalIgnoreCase))
                    {
                        // The file has a matching extension
                        //Command: renamer *jpg* *png*
                        //Result: change extension of all files
                        return Path.ChangeExtension(fileName, replacedSecondPattern);
                    }
                    else
                    {
                        Console.WriteLine("No matching file extension");
                    }
                }
                else if (SourceStartsWithNumber && Path.GetExtension(fileName) == Path.GetExtension(sourceFilePattern))
                {
                    string outputString = Regex.Replace(fname, @"(\d+)-(.*)", @"$2-$1");

                    return outputString + Path.GetExtension(fileName);
                }
                else if (splitStrings(fname)[0] == splitStrings(sourceFilePattern)[0] && Path.GetExtension(fileName) == splitStrings(sourceFilePattern)[2])
                {
                    if (startsWithNumber)
                    {
                        //Command: renamer img-1.jpg 1-img.jpg
                        //Result: 1-img.jpg
                        return MoveNumbersToFront(fileName);
                    }
                    string newFilename = fileName.Replace(replacedFirstPattern, replacedSecondPattern);

                    if (!File.Exists(newFilename))
                    {
                        return newFilename;
                    }

                    string num = splitStrings(fname)[1];                 
                    string[] parts = GetStringParts(destinationFilePattern);

                    //Command: renamer img1.jpg img-1.jpg
                    //Result: img-1.jpg
                    return fileName.Replace(fname, parts[0] + parts[1] + num);
                }

                //Command: renamer img**
                //Result: removes img of all files
                return fileName.Replace(replacedFirstPattern, replacedSecondPattern);

            } else if (sourceFilePattern.StartsWith("*"))
            {
                return fname;
            }
         
            return fileName.Substring(match.Index + match.Length);
        }

        public static string MoveNumbersToFront(string fileName)
        {
            //renamer img-123.jpg 123-img.jpg
            string fname = Path.GetFileNameWithoutExtension(fileName);

            string[] parts = GetStringParts(fname);

            return fileName.Replace(fname, parts[2] + parts[1] + parts[0]);
        }

        public static string[] GetStringParts(string fname)
        {
            string pattern1 = @"(\D+)([-_]+)(\d+)(\.\w+)?";
            // Regular expression pattern to match the desired characters
            Match match1 = Regex.Match(fname, pattern1);
            string[] parts = new string[4];

            if (match1.Success)
            {
                // Extract the matched groups
                parts[0] = match1.Groups[1].Value;     // String part
                parts[1] = match1.Groups[2].Value;     // Delimiter
                parts[2] = match1.Groups[3].Value;     // Numeric part
                parts[3] = match1.Groups[4].Value;     // File extension (optional)
            }

            return parts;
        }

        public static string[] splitStrings(string sourceFilePattern)
        {
            string[] parts = new string[3];
            int index = 0;

            // Find the index where the numeric part starts
            for (int i = 0; i < sourceFilePattern.Length; i++)
            {
                if (char.IsDigit(sourceFilePattern[i]))
                {
                    index = i;
                    break;
                }
            }

            // Extract the string part
            parts[0] = sourceFilePattern.Substring(0, index);

            // Find the index where the extension starts
            int extIndex = sourceFilePattern.LastIndexOf('.');
            if (extIndex != -1)
            {
                // Extract the numeric part
                parts[1] = sourceFilePattern.Substring(index, extIndex - index);

                // Extract the extension
                parts[2] = sourceFilePattern.Substring(extIndex);
            }
            else
            {
                // If there is no extension, the remaining part is the numeric part
                parts[1] = sourceFilePattern.Substring(index);
            }

            return parts;

        }
    }
}
