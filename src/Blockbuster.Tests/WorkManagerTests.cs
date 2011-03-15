using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using System.IO;
using System.Reactive;
using Blockbuster.DirectoryIterator;
using Blockbuster.Core;
using Blockbuster.Commands;
using Blockbuster.Commands.Filtering;
using Blockbuster.Contracts;

namespace Blockbuster.Tests
{
	[TestFixture]
	public class WorkManagerTests
	{
		[Test]
		public void WorkManagerInvokedWithNoFilterShouldDeleteAllFiles()
		{
			int deleted = 0;
			var dummyLoggerScrapHeap = new DummyLoggerScrapHeap();

			WorkManager workManager = new WorkManager(new MockFileSystemIterator(), dummyLoggerScrapHeap);
			workManager.Delete("source");
			dummyLoggerScrapHeap.LoggingStream.Subscribe(x => deleted++);

			Assert.AreEqual(3, deleted);
		}

		[Test]
		public void WorkManagerInvokedWithDeleteNothingShouldDeleteNoFiles()
		{
			int deleted = 0;
			var dummyLoggerScrapHeap = new DummyLoggerScrapHeap();

			WorkManager workManager = new WorkManager(new MockFileSystemIterator(), dummyLoggerScrapHeap);
			workManager.Delete("source", new List<AbstractCommand>() { new DeleteNothing() });
			dummyLoggerScrapHeap.LoggingStream.Subscribe(x => deleted++);

			Assert.AreEqual(0, deleted);
		}

		[Test]
		public void WorkManagerInvokedWithHasDateShouldDeleteOnlySpecificEntities()
		{
			int deleted = 0;
			var dummyLoggerScrapHeap = new DummyLoggerScrapHeap();

			WorkManager workManager = new WorkManager(new MockFileSystemIterator(), dummyLoggerScrapHeap);
			workManager.Delete("source", new List<AbstractCommand>() 
			{ 
				new HasDate(x => x.Year == 2011 && x.Month == 3 && x.Day == 13)
			});
			dummyLoggerScrapHeap.LoggingStream.Subscribe(x => deleted++);

			Assert.AreEqual(2, deleted);
		}

		[Test]
		public void WorkManagerInvokedWithHasDateAndFilesOnlyShouldDeleteOnlySpecificEntities()
		{
			int deleted = 0;
			var dummyLoggerScrapHeap = new DummyLoggerScrapHeap();

			WorkManager workManager = new WorkManager(new MockFileSystemIterator(), dummyLoggerScrapHeap);
			workManager.Delete("source",new List<AbstractCommand>() 
			{ 
				new HasDate(x => x.Year == 2011 && x.Month == 3 && x.Day == 13),
				new FilesOnly()
			});
			dummyLoggerScrapHeap.LoggingStream.Subscribe(x => deleted++);

			Assert.AreEqual(1, deleted);
		}

		[Test]
		public void WorkManagerInvokedWithHasDateAndDirectoriesOnlyShouldDeleteOnlySpecificEntities()
		{
			int deleted = 0;
			var dummyLoggerScrapHeap = new DummyLoggerScrapHeap();

			WorkManager workManager = new WorkManager(new MockFileSystemIterator(), dummyLoggerScrapHeap);
			workManager.Delete("source", new List<AbstractCommand>() 
			{ 
				new HasDate(x => x.Year == 2011 && x.Month == 3 && x.Day == 13),
				new DirectoriesOnly()
			});
			dummyLoggerScrapHeap.LoggingStream.Subscribe(x => deleted++);

			Assert.AreEqual(1, deleted);
		}

		[Test]
		public void WorkManagerInvokedWithDirectoriesOnlyAndFilesOnlyShouldDeleteNothing()
		{
			int deleted = 0;
			var dummyLoggerScrapHeap = new DummyLoggerScrapHeap();

			WorkManager workManager = new WorkManager(new MockFileSystemIterator(), dummyLoggerScrapHeap);
			workManager.Delete("source", new List<AbstractCommand>() 
			{ 
				new DirectoriesOnly(),
				new FilesOnly()
			});
			dummyLoggerScrapHeap.LoggingStream.Subscribe(x => deleted++);

			Assert.AreEqual(0, deleted);
		}

		[Test]
		public void WorkManagerInvokedWithFileExtensionTxtShouldDeleteOnlySpecificEntities()
		{
			int deleted = 0;
			var dummyLoggerScrapHeap = new DummyLoggerScrapHeap();

			WorkManager workManager = new WorkManager(new MockFileSystemIterator(), dummyLoggerScrapHeap);
			workManager.Delete("source", new List<AbstractCommand>() 
			{ 
				new FileExtension("txt")
			});
			dummyLoggerScrapHeap.LoggingStream.Subscribe(x => deleted++);

			Assert.AreEqual(1, deleted);
		} 
	}

	public class MockFileSystemIterator : IFileSystemIterator
	{
		public IObservable<FileSystemEntity> Iterate(string directory) 
		{
			var directoryInfo1 = Rhino.Mocks.MockRepository.GenerateMock<DirectoryInfo>();
			directoryInfo1.Stub(x => x.CreationTime).Return(new DateTime(2011, 3, 13));

			var directoryInfo2 = Rhino.Mocks.MockRepository.GenerateMock<DirectoryInfo>();
			directoryInfo2.Stub(x => x.CreationTime).Return(new DateTime(2011, 3, 12));
			
			var fileInfo1 = Rhino.Mocks.MockRepository.GenerateMock<FileInfo>();
			fileInfo1.Stub(x => x.CreationTime).Return(new DateTime(2011, 3, 13));
			fileInfo1.Stub(x => x.Extension).Return("txt");
			
			List<FileSystemEntity> fileSystemEntities = new List<FileSystemEntity>()
			{
				new FileSystemEntity(directoryInfo1),
				new FileSystemEntity(directoryInfo2),
				new FileSystemEntity(fileInfo1)
			};
			return fileSystemEntities.ToObservable();
		}
	}
}
