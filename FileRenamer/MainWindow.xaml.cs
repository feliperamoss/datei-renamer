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

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)folderComboBox1.SelectedItem;
            string selectedItemContent = selectedComboBoxItem.Content.ToString();
            string sourcePattern;
            string destinationPattern;

            if (selectedItemContent == "Add hyphen to name")
            {
                sourcePattern = originalNameTextBox.Text;
                destinationPattern = newNameTextBox.Text;
                RenameFilesAndDisplay(sourcePattern, destinationPattern, "Filename was upated successfully!");
            }
            else if (selectedItemContent == "Change prefix")
            {
                sourcePattern = originalNameTextBox.Text;
                destinationPattern = newNameTextBox.Text;
                RenameFilesAndDisplay(sourcePattern, destinationPattern, "Filename was upated successfully!");
            }
            else if (selectedItemContent == "Remove substring")
            {
                sourcePattern = originalNameTextBox.Text;
                RenameFilesAndDisplay(sourcePattern, "", "Filename was upated successfully!");
            }
            else if (selectedItemContent == "Move number to front")
            {
                sourcePattern = originalNameTextBox.Text;
                destinationPattern = newNameTextBox.Text;
                RenameFilesAndDisplay(sourcePattern, destinationPattern, "Filename was upated successfully!");
            }
            else if (selectedItemContent == "Change file type") // "Change Prefix" selected
            {
                sourcePattern = "*." + originalNameTextBox.Text + "*";
                destinationPattern = "*." + newNameTextBox.Text + "*";
                RenameFilesAndDisplay(sourcePattern, destinationPattern, "File type was upated successfully!");
            }
            else if (selectedItemContent == "Remove file type") // "Change Prefix" selected
            {
                sourcePattern = "*." + originalNameTextBox.Text + "*";
                RenameFilesAndDisplay(sourcePattern, "", "File type was removed successfully!");
            }

        }

        private void RenameFilesAndDisplay(string sourcePattern, string destinationPattern, string message)
        {
            string selectedFolder = selectFolderTextBox.Text;
            List<FileData> fileDataList = FileHelper.RenameFiles(selectedFolder, sourcePattern, destinationPattern);

            // Refresh the ListView
            try
            {
                fileListView.ItemsSource = fileDataList;
                CollectionViewSource.GetDefaultView(fileListView.ItemsSource).Refresh();

                bool anyUpdated = fileDataList.Any(fileData => fileData.ChangeStatus.Contains("Updated"));

                if (anyUpdated)
                {
                    MessageBox.Show(message, "Operation Completed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No files matching the specified criteria found. No files were updated.", "Operation Completed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during file renaming:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FolderComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedComboBoxItem = (ComboBoxItem)folderComboBox1.SelectedItem;
            string selectedItemContent = selectedComboBoxItem.Content.ToString();

            if (selectedItemContent == "Change file type")
            {
                originalNameLabel.Text = "Original Extension:";
                newNameLabel.Text = "New Extension:";
                newNameTextBox.Visibility = Visibility.Visible;  // Hide the input field

            }
            else if (selectedItemContent == "Remove file type")
            {
                originalNameLabel.Text = "Original Extension:";
                newNameLabel.Text = "";  // Remove the label text (optional)
                newNameTextBox.Visibility = Visibility.Collapsed;  // Hide the input field
                newNameTextBox.Text = "";  // Clear the input field's value (optional)
            }
            else if (selectedItemContent == "Add hyphen to name" ||
                selectedItemContent == "Change prefix" || selectedItemContent == "Remove substring" || selectedItemContent == "Move number to front")
            {
                originalNameLabel.Text = "Original Name:";
                newNameLabel.Text = "New Name:"; 
                newNameTextBox.Visibility = Visibility.Visible;  // Hide the input field
            }
        }
    }
}
