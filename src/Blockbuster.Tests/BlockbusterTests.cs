using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Blockbuster;
using Blockbuster.Commands;
using Blockbuster.Commands.Filtering;

namespace Blockbuster.Tests
{
	[TestFixture]
	public class BlockbusterTests
	{
		[Test]
		public void DeletesCompleteDirectory()
		{
			string path = @"D:\git\BlockbusterX\src\Blockbuster.Tests\bin\Debug\Tests";
			DirectoryInfo directory = new DirectoryInfo(path);
			Assert.That(directory.GetFiles().Count<FileInfo>() > 0,"no testfiles present");
			Blockbuster service = new Blockbuster();
			service.CleanUp(path);
			Assert.That(directory.GetFiles().Count<FileInfo>() == 0);
		}

		[Test]
		public void DeletesOnlyFiles()
		{
			string path = @"D:\git\BlockbusterX\src\Blockbuster.Tests\bin\Debug\Tests";
			DirectoryInfo directory = new DirectoryInfo(path);
			Assert.That(directory.GetFiles().Count<FileInfo>() > 0, "no testfiles present");
			AbstractCommand[] commands = { new FilesOnly() };
			Blockbuster service = new Blockbuster();
			service.CleanUp(path, commands);
			Assert.That(directory.GetFiles().Count<FileInfo>() == 0);
		}
	}   
}
