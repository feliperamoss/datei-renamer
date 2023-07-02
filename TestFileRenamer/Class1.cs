using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileRenamer;
using System.IO.Abstractions;

namespace TestFileRenamer
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void RenameFiles_RenamesFilesWithMatchingPattern()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file1.txt"), "File 1 content");
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file2.txt"), "File 2 content");

            // Act
            FileHelper.RenameFiles(testDirectory, "file1.txt", "renamed-1.txt");

            // Assert
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "renamed-1.txt")));
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "renamed-2.txt")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "renamed-1.txt"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "renamed-2.txt"));
            fileSystem.Directory.Delete(testDirectory, true);
        }

        [Test]
        public void RenameFiles_ThrowsExceptionWhenDestinationFileExists()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file1.txt"), "File 1 content");
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "renamed-1.txt"), "Renamed File 1 content");

            // Act and Assert
            Assert.Throws<IOException>(() =>
            {
                FileHelper.RenameFiles(testDirectory, "file1.txt", "renamed-1.txt");
            });

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "file1.txt"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "renamed-1.txt"));
            fileSystem.Directory.Delete(testDirectory, true);
        }

        [Test]
        public void RenameFiles_IgnoresFilesWithDifferentExtension()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file1.txt"), "File 1 content");
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file1.jpg"), "File 1 image content");

            // Act
            FileHelper.RenameFiles(testDirectory, "file1.txt", "renamed-1.txt");

            // Assert
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "renamed-1.txt")));
            Assert.IsFalse(fileSystem.File.Exists(Path.Combine(testDirectory, "renamed-1.jpg")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "renamed-1.txt"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "file1.jpg"));
            fileSystem.Directory.Delete(testDirectory, true);
        }

        [Test]
        public void RenameFiles_UpdatesFileNameWithNumberMovedToFront()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "img-123.jpg"), "Image 123 content");

            // Act
            FileHelper.RenameFiles(testDirectory, "img-123.jpg", "123-img.jpg");

            // Assert
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "123-img.jpg")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "123-img.jpg"));
            fileSystem.Directory.Delete(testDirectory, true);
        }

        [Test]
        public void RenameFiles_UpdatesFileNameWithNumberMovedToFrontWhenMultipleFilesPresent()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-1.txt"), "File 1 content");
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-2.txt"), "File 2 content");

            // Act
            FileHelper.RenameFiles(testDirectory, "file-1.txt", "1-file.txt");

            // Assert
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "1-file.txt")));
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "2-file.txt")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "1-file.txt"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "2-file.txt"));
            fileSystem.Directory.Delete(testDirectory, true);
        }

        [Test]
        public void RenameFiles_ChangeFileExtension()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-1.txt"), "File 1 content");
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-2.txt"), "File 2 content");

            // Act
            FileHelper.RenameFiles(testDirectory, "*.txt*", "*.png*");

            // Assert
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "file-1.png")));
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "file-2.png")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "file-1.png"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "file-2.png"));
            fileSystem.Directory.Delete(testDirectory, true);
        }

        [Test]
        public void RenameFiles_RemovePrefix()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-1.txt"), "File 1 content");
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-2.txt"), "File 2 content");

            // Act
            FileHelper.RenameFiles(testDirectory, "file-**");

            // Assert
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "1.txt")));
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "2.txt")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "1.txt"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "2.txt"));
            fileSystem.Directory.Delete(testDirectory, true);
        }

        [Test]
        public void RenameFiles_RemoveFileExtension()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-1.txt"), "File 1 content");
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-2.txt"), "File 2 content");

            // Act
            FileHelper.RenameFiles(testDirectory, "*.txt*");

            // Assert
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "file-1")));
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "file-2")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "file-1"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "file-2"));
            fileSystem.Directory.Delete(testDirectory, true);
        }

        [Test]
        public void RenameFiles_UpdateFilenamePrefix()
        {
            // Arrange
            var fileSystem = new FileSystem();
            var testDirectory = "C:\\Documents\\FakeTestDirectory";
            fileSystem.Directory.CreateDirectory(testDirectory);
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-1.txt"), "File 1 content");
            fileSystem.File.WriteAllText(Path.Combine(testDirectory, "file-2.txt"), "File 2 content");

            // Act
            FileHelper.RenameFiles(testDirectory, "file-*", "foo-*");

            // Assert
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "foo-1.txt")));
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "foo-2.txt")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "foo-1.txt"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "foo-2.txt"));
            fileSystem.Directory.Delete(testDirectory, true);
        }
    }
}
