using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace FileRenamer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Contains("renamer"))
            {
                GetFiles();
                return;
            }

            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Select a folder."
            };

            if (dialog.ShowDialog() == true)
            {
                string selectedFolder = System.IO.Path.GetDirectoryName(dialog.FileName);
                selectFolderTextBox.Text = selectedFolder;
            }
        }

        static void GetFiles()
        {
            string directoryPath = Directory.GetCurrentDirectory(); // Replace with the actual directory path

            // Check if the specified directory exists
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory not found.");
                return;
            }
            try
            {
                // Get all files in the directory
                string[] files = Directory.GetFiles(directoryPath);

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
