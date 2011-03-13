using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.CommonTypes;

namespace Blockbuster.Core
{
    public class ScrapHeap : IScrapHeap, IDisposable
    {
        IDisposable _subscription = null;

        public void BurnScrap(IObservable<FileSystemEntity> source)
        {
            _subscription = source.Subscribe(entity =>
            {
                if (entity.File != null)
                    entity.File.Delete();
                else
                    entity.Directory.Delete(true);
            });
        }

        public void Dispose()
        {
            if (_subscription != null)
                _subscription.Dispose();
        }
    }
}
