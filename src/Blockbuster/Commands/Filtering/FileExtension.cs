using System;
using System.Linq;
using Blockbuster.Contracts;

namespace Blockbuster.Commands.Filtering
{
    /// <summary>
    /// Description of FileExtension.
    /// </summary>
    public class FileExtension : AbstractCommand
    {
        public override string Name { get { return "FileExtension"; } }

        public FileExtension(string fileExtension) 
        {
            AdditionalParameters = fileExtension;
        }

        public FileExtension()
        {
        }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => x.Type == FileSystemEntity.FileSystemEntityType.File && x.File.Extension == (string)AdditionalParameters);
        }
    }
}
