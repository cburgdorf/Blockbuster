using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Blockbuster.CommonTypes;

namespace Blockbuster.DirectoryIterator
{
	public class ObservableFileAndDirectoryIterator : IFileSystemIterator
	{
		public IObservable<FileSystemEntity> Iterate(string directoryFullName)
		{
			DirectoryInfo entry = new DirectoryInfo(directoryFullName);

			//Create a new data stream of FileSystemEntities (becoming our core data stream)
			var observableFileSystemEntities = Observable.Empty<FileSystemEntity>();

			//Create a new data stream of DirectoryInfos from the command root directory
			var observableDirectories = entry.EnumerateDirectories("*", SearchOption.AllDirectories).ToObservable();

			//Create a new data stream of FileSystemEntities based on the files laying inside the command root top level directory 
			var observableFirstLevelFiles = entry.EnumerateFiles("*", SearchOption.TopDirectoryOnly)
												.ToObservable()
												.Select(x => new FileSystemEntity(x));

			//merge previous data stream into our core data stream of FileSystemEntities
			observableFileSystemEntities = observableFileSystemEntities.Merge(observableFirstLevelFiles);

			//Based on our stream of DirectoryInfos, project it into a stream of FileSystemEntities and merge it
			//into our core data stream
			observableFileSystemEntities = observableFileSystemEntities.Merge(observableDirectories.Select(x => new FileSystemEntity(x)));

			//Based on our stream of DirectoryInfos, project each stream of FileInfos into a flat stream
			//of FileInfos, project this into a stream of FileSystemEntities and merge it into our core stream
			var observableFiles = observableDirectories.SelectMany(x => x.EnumerateFiles().ToObservable()).Select(x => new FileSystemEntity(x));
			observableFileSystemEntities = observableFileSystemEntities.Merge(observableFiles);

			//return our core data stream
			return observableFileSystemEntities;
		}
	}
}
