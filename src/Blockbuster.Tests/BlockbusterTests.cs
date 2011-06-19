using System;
using System.Collections.Generic;
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

namespace Blockbuster.Tests
{
	[TestFixture]
	public class BlockbusterTests
	{
		private string _testFilePath;

		[SetUp]
		public void SetUp()
		{
			_testFilePath = Environment.CurrentDirectory + @"\Tests";
		}

		[Test]
		public void DeletesCompleteDirectory()
		{
			TestFileGenerator.Generate(_testFilePath, 3, 2);
			DirectoryInfo directory = new DirectoryInfo(_testFilePath);
			Assert.That(directory.GetFiles().Count<FileInfo>() > 0,"no testfiles present");
			Blockbuster service = new Blockbuster();
			service.CleanUp(_testFilePath);
			Assert.That(directory.GetFiles().Count<FileInfo>() == 0);
		}

		[Test]
		public void DeletesOnlyFiles()
		{
			TestFileGenerator.Generate(_testFilePath, 3, 2);
			DirectoryInfo directory = new DirectoryInfo(_testFilePath);
			Assert.That(directory.GetFiles().Count<FileInfo>() > 0, "no testfiles present");
			AbstractCommand[] commands = { new FilesOnly() };
			Blockbuster service = new Blockbuster();
			service.CleanUp(_testFilePath, commands);
			Assert.That(directory.GetFiles().Count<FileInfo>() == 0);
		}
	}   
}
