using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.CommonTypes;

namespace Blockbuster.Core
{
    public class DummyLoggerScrapHeap : IScrapHeap
    {        

        public IObservable<FileSystemEntity> LoggingStream { get; private set; }

        public void BurnScrap(IObservable<FileSystemEntity> source)
        {
            LoggingStream = source;
        }
    }
}
