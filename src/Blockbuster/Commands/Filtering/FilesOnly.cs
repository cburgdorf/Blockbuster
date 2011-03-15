using System;
using System.Linq;
using Blockbuster.Contracts;

namespace Blockbuster.Commands.Filtering
{
    public class FilesOnly: AbstractCommand
    {
        public override string Name { get { return "FilesOnly"; } }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => x.Type == FileSystemEntity.FileSystemEntityType.File);
        }
    }
}
