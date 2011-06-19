using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using Blockbuster;
using Blockbuster.Commands;
using Blockbuster.Commands.Filtering;
using Blockbuster.Contracts;
using Rhino.Mocks;

namespace Blockbuster.Tests
{
	[TestFixture]
	public class BlockbusterDynamicConfigurationIntegrationTests
	{
	    private string _testFilePath;

        [SetUp]
	    public void SetUp()
	    {
            _testFilePath = Environment.CurrentDirectory + @"\Tests";
	    }

	    [Test]
		public void DeletesFilesOnly()
		{
		    TestFileGenerator.Generate(_testFilePath, 3, 2);
            var directory = new DirectoryInfo(_testFilePath);
			Assert.That(directory.GetFiles().Count<FileInfo>() > 0,"no testfiles present");
			var service = new Blockbuster();
	        var dynamicConfiguration = MockRepository.GenerateStub<IDynamicConfiguration>();
            dynamicConfiguration.Stub(x => x.GetCommandConfigurations()).Return(new[] {string.Format("CommandName='FilesOnly'; Directory='{0}'", _testFilePath)});
            service.CleanUp(dynamicConfiguration, false);
			Assert.That(directory.GetFiles().Count<FileInfo>() == 0);
		}


        [Test]
        public void DeletesTxtFilesButKeepsBakFiles()
        {
            var txtFileInfo = new FileInfo(_testFilePath + "\\test.txt");
            using (txtFileInfo.Create()) { }

            var bakFileInfo = new FileInfo(_testFilePath + "\\test.bak");
            using (bakFileInfo.Create()) { }

            var directory = new DirectoryInfo(_testFilePath);
            Assert.That(directory.GetFiles().Count<FileInfo>() > 0, "no testfiles present");
            var service = new Blockbuster();
            var dynamicConfiguration = MockRepository.GenerateStub<IDynamicConfiguration>();
            dynamicConfiguration.Stub(x => x.GetCommandConfigurations()).Return(new[] { string.Format("CommandName='FileExtension'; FileExtension='txt'; Directory='{0}'", _testFilePath) });
            service.CleanUp(dynamicConfiguration, true);
            Assert.That(directory.GetFiles().Count<FileInfo>() == 1);
        }
	}   
}
