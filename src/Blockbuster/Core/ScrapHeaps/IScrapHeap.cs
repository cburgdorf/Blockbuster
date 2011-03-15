using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;

namespace Blockbuster.Core
{
    public interface IScrapHeap
    {
        void BurnScrap(IObservable<FileSystemEntity> source);
    }
}
