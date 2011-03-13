using System;
using System.Linq;
using Blockbuster.CommonTypes;

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

        public override IObservable<CommonTypes.FileSystemEntity> FilterFileSystemEntities(IObservable<CommonTypes.FileSystemEntity> source)
        {
            return source.Where(x => x.Type == CommonTypes.FileSystemEntity.FileSystemEntityType.File && x.File.Extension == (string)AdditionalParameters);
        }
    }
}
