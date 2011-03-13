using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.CommonTypes;

namespace Blockbuster.Core
{
    public interface IScrapHeap
    {
        void BurnScrap(IObservable<FileSystemEntity> source);
    }
}
