using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.CommonTypes;

namespace Blockbuster.DirectoryIterator
{
    public interface IFileSystemIterator
    {
        IObservable<FileSystemEntity> Iterate(string directoryFullName);
    }
}
