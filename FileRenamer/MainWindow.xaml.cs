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
            InitializeComponent();

            /**string[] args = Environment.GetCommandLineArgs();
            if (args.Contains("renamer"))
            {
                string directoryPath = Directory.GetCurrentDirectory();
                FileHelper.RenameFiles(directoryPath, args[1]);
                Close(); // Close the application after executing the command-line logic
            }**/
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
                fileListView.ItemsSource = FileHelper.GetFilesForInterface(selectedFolder);
            }
        }


    }
}
