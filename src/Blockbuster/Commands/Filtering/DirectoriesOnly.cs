
using System;
using System.Linq;
using Blockbuster.Contracts;

namespace Blockbuster.Commands.Filtering
{
    public class DirectoriesOnly: AbstractCommand
    {
        public override string Name { get { return "DirectoriesOnly"; } }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => x.Type == FileSystemEntity.FileSystemEntityType.Directory);
        }
    }
}
