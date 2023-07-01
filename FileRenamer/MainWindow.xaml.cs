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
        private List<FileData> fileDataList;

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

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFolder = selectFolderTextBox.Text;
            //string sourcePattern = "*";
            //string destinationPattern = string.Empty;
            string sourcePattern;
            string destinationPattern;

            if (folderComboBox1.SelectedIndex == 2) // "Remove Suffix" selected
            {
                sourcePattern = "*.*";
            }
            else if (folderComboBox1.SelectedIndex == 3) // "Change Prefix" selected
            {
                sourcePattern = originalNameTextBox.Text;
                destinationPattern = newNameTextBox.Text;
                List<FileData> fileDataList = FileHelper.RenameFiles(selectedFolder, sourcePattern, destinationPattern);

                // Refresh the ListView
                fileListView.ItemsSource = fileDataList;
                CollectionViewSource.GetDefaultView(fileListView.ItemsSource).Refresh();
                MessageBox.Show("Filename was upated successfully!");
            }
            else if (folderComboBox1.SelectedIndex == 4) // "Change Prefix" selected
            {
                sourcePattern = "*." + originalNameTextBox.Text + "*";
                destinationPattern = "*." + newNameTextBox.Text + "*";
                List<FileData> fileDataList = FileHelper.RenameFiles(selectedFolder, sourcePattern, destinationPattern);

                // Refresh the ListView
                fileListView.ItemsSource = fileDataList;
                CollectionViewSource.GetDefaultView(fileListView.ItemsSource).Refresh();
                MessageBox.Show("File type was upated successfully!");
            }

        }

        private void FolderComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)folderComboBox1.SelectedItem;
            string selectedItemContent = selectedComboBoxItem.Content.ToString();

            if (selectedItemContent == "Change Sufix")
            {
                originalNameLabel.Text = "Original Extension:";
                newNameLabel.Text = "New Extension:";
            }
        }

    }
}
