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

        public string ExtensionOfFile { get; protected set; }

        public FileExtension(string fileExtension)
        {
            ExtensionOfFile = fileExtension;
        }

        public FileExtension()
        {
        }

        public override System.Collections.Generic.Dictionary<string, object> Configuration
        {
            get
            {
                return base.Configuration;
            }
            set
            {
                base.Configuration = value;
                if (value.ContainsKey("FileExtension"))
                    ExtensionOfFile = (string)value["FileExtension"];
            }
        }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => x.Type == FileSystemEntity.FileSystemEntityType.File && x.File.Extension == ExtensionOfFile);
        }
    }
}
