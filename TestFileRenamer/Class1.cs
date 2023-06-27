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
        public void TestMoveNumberToFront()
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
            Assert.IsTrue(fileSystem.File.Exists(Path.Combine(testDirectory, "2.txt")));

            // Clean up
            fileSystem.File.Delete(Path.Combine(testDirectory, "1-file.txt"));
            fileSystem.File.Delete(Path.Combine(testDirectory, "2.txt"));
            fileSystem.Directory.Delete(testDirectory, true);
        }
    }
}
