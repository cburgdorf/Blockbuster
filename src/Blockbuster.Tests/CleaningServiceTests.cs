using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Blockbuster;

namespace Blockbuster.Tests
{
    [TestFixture]
    public class CleaningServiceTests
    {
        [Test]
        public void DeletesCompleteDirectory()
        {
            string path = @"D:\git\utilities\src\Blockbuster.Tests\bin\Debug\Tests";
            DirectoryInfo directory = new DirectoryInfo(path);
            Assert.That(directory.GetFiles().Count<FileInfo>() > 0,"no testfiles present");
            CleanUpService service = new CleanUpService();
            service.CleanUp(path);
            Assert.That(directory.GetFiles().Count<FileInfo>() == 0);
        }

        [Test]
        public void DeletesOnlyFiles()
        {
            string path = @"D:\git\utilities\src\Blockbuster.Tests\bin\Debug\Tests";
            DirectoryInfo directory = new DirectoryInfo(path);
            Assert.That(directory.GetFiles().Count<FileInfo>() > 0, "no testfiles present");
            CleanUpService service = new CleanUpService();
            service.CleanUp(path, "FilesOnly",null);
            Assert.That(directory.GetFiles().Count<FileInfo>() == 0);
        }
    }   
}
