using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Blockbuster.Contracts
{
    public class FileSystemEntity
    {
        public enum FileSystemEntityType
        {
            Directory,
            File
        }

        public FileSystemEntity(FileInfo fileInfo) 
        {
            File = fileInfo;
            Type = FileSystemEntityType.File;
        }

        public FileSystemEntity(DirectoryInfo directoryInfo)
        {
            Directory = directoryInfo;
            Type = FileSystemEntityType.Directory;
        }

        public DirectoryInfo Directory { get; private set; }
        public FileInfo File { get; private set; }

        public FileSystemEntityType Type { get; private set; }

        public DateTime CreationTime 
        {
            get { return Type == FileSystemEntityType.Directory ? Directory.CreationTime : File.CreationTime; } 
        }
    }
}
