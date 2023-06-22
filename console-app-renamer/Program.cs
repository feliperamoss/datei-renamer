using System;

namespace console_app_renamer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Souliyo function
            //static void Umbenennen()
            //{
            //    string oldFilePath = "C:\\Users\\Souli\\Documents\\Berufsschule\\Software-Projekt\\Test-Ordner\\Rename-Folder-Test\\AK 2__Themen E1  Wi+GK.docx";
            //    string newFilePath = "C:\\Users\\Souli\\Documents\\Berufsschule\\Software-Projekt\\Test-Ordner\\Rename-Folder-Test\\KA 2__Themen E1  Wi+GK.docx";

            //    File.Move(oldFilePath, newFilePath);
            //}

            string FilePath;
            string oldFile;
            string newFile;

            // Optional falls nur bestimmte Datei-Format umbenannt werden will.
            string[] format = new string[7] { ".img", ".png", ".docx", ".jpg", ".txt" };

            FilePath = selectFolderTextBox.Text;
            oldFile = originalNameTextBox.Text;
            newFile = newNameTextBox.Text;

            string oldFilePath = FilePath + "\\" + oldFile;
            string newFilePath = FilePath + "\\" + newFile;

            File.Move(oldFilePath, newFilePath);
        }
    }
}

