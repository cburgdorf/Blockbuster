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
			AssertEmptyTestDirectory();
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
			AssertEmptyTestDirectory();

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
			Assert.That(!txtFileInfo.Exists);
			Assert.That(bakFileInfo.Exists);
		}

		[Test]
		public void DeletesTxtFilesOlderDateButKeepsBakFilesWithSameDate()
		{
			AssertEmptyTestDirectory();
			
			var txtFileInfo = new FileInfo(_testFilePath + "\\test.txt");
			using (txtFileInfo.Create()) { }

			var bakFileInfo = new FileInfo(_testFilePath + "\\test.bak");
			using (bakFileInfo.Create()) { }

			var directory = new DirectoryInfo(_testFilePath);
			Assert.That(directory.GetFiles().Count<FileInfo>() > 0, "no testfiles present");
			var service = new Blockbuster();
			var dynamicConfiguration = MockRepository.GenerateStub<IDynamicConfiguration>();
			Thread.Sleep(1000); //Sleep for one second to force files to be older
			dynamicConfiguration.Stub(x => x.GetCommandConfigurations()).Return(new[]
																					{
																						string.Format("CommandName='FileExtension'; FileExtension='txt'; Directory='{0}'", _testFilePath),
																						string.Format("CommandName='IsOlder'; IsOlder='{0}'; Directory='{1}'", DateTime.Now, _testFilePath)
																					});
			service.CleanUp(dynamicConfiguration, true);
			Assert.That(!txtFileInfo.Exists);
			Assert.That(bakFileInfo.Exists);
		}

		[Test]
		public void DeletesFilesInBetweenSpecificTimeRangeButKeepsOthers()
		{
			AssertEmptyTestDirectory();

			var txtFileInfo = new FileInfo(_testFilePath + "\\test.txt");
			using (txtFileInfo.Create()) { }

			var dateTimeTimeRangeStart = DateTime.Now;

			Thread.Sleep(1000);
			
			var txtFileInfo2 = new FileInfo(_testFilePath + "\\test2.txt");
			using (txtFileInfo2.Create()) { }

			Thread.Sleep(1000);

			var dateTimeTimeRangeStop = DateTime.Now;

			var txtFileInfo3 = new FileInfo(_testFilePath + "\\test3.txt");
			using (txtFileInfo3.Create()) { }
			
			var directory = new DirectoryInfo(_testFilePath);
			Assert.That(directory.GetFiles().Count<FileInfo>() > 0, "no testfiles present");
			var service = new Blockbuster();
			var dynamicConfiguration = MockRepository.GenerateStub<IDynamicConfiguration>();
			Thread.Sleep(1000); //Sleep for one second to force files to be older
			dynamicConfiguration.Stub(x => x.GetCommandConfigurations()).Return(new[]
																					{
																						string.Format("CommandName='FileExtension'; FileExtension='txt'; Directory='{0}'", _testFilePath),
																						string.Format("CommandName='IsNewer'; IsNewer='{0}'; Directory='{1}'", dateTimeTimeRangeStart, _testFilePath),
																						string.Format("CommandName='IsOlder'; IsOlder='{0}'; Directory='{1}'", dateTimeTimeRangeStop, _testFilePath)
																					});
			service.CleanUp(dynamicConfiguration, true);
			Assert.That(txtFileInfo.Exists);
			Assert.That(!txtFileInfo2.Exists);
			Assert.That(txtFileInfo3.Exists);
		}


		private void AssertEmptyTestDirectory()
		{
			var service = new Blockbuster();
			service.CleanUp(_testFilePath);
			var directoryInfo = new DirectoryInfo(_testFilePath);

			Assert.AreEqual(0, directoryInfo.GetFiles().Count());
			Assert.AreEqual(0, directoryInfo.GetDirectories().Count());
		}
	}   
}
