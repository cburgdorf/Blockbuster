using System;
using System.Collections.Generic;
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

        public override void Configure(Dictionary<string, string> value)
        {
            if (value.ContainsKey("fileextension"))
                ExtensionOfFile = value["fileextension"];
        }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => x.Type == FileSystemEntity.FileSystemEntityType.File && x.File.Extension == ExtensionOfFile);
        }
    }
}
