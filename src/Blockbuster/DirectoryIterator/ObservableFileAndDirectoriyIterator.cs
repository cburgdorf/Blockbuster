using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Blockbuster.CommonTypes;

namespace Blockbuster.DirectoryIterator
{
    public class ObservableFileAndDirectoryIterator : IFileSystemIterator
    {
        public IObservable<FileSystemEntity> Iterate(string directoryFullName)
        {
            DirectoryInfo entry = new DirectoryInfo(directoryFullName);

            //Frischen Datenstrom von FileSystemEntities erstellen
            var observableFileSystemEntities = Observable.Empty<FileSystemEntity>();

            //Datenstrom von DirectoryInfos ausgehend vom Root Directory erstellen
            var observableDirectories = entry.EnumerateDirectories("*", SearchOption.AllDirectories).ToObservable();

            //Datenstrom aus Dateien im TopLevelDirectory erstellen
            var observableFirstLevelFiles = entry.EnumerateFiles("*", SearchOption.TopDirectoryOnly)
                                                .ToObservable()
                                                .Select(x => new FileSystemEntity(x));

            //Datenstrom der TopLevel Files in den HauptDatenstrom mergen
            observableFileSystemEntities = observableFileSystemEntities.Merge(observableFirstLevelFiles);

            //Datenstrom von FileSystemEntities auf Basis der DirectoryInfos in den Hauptdatenstrom
            //von FileSystemEntities mergen
            observableFileSystemEntities = observableFileSystemEntities.Merge(observableDirectories.Select(x => new FileSystemEntity(x)));

            //Datenstrom von FileSystemEntities auf Basis der FileInfos aller Directories
            //erstellen und in den Hauptdatenstrom von FileSystemEntities mergen
            var observableFiles = observableDirectories.SelectMany(x => x.EnumerateFiles().ToObservable()).Select(x => new FileSystemEntity(x));
            observableFileSystemEntities = observableFileSystemEntities.Merge(observableFiles);

            return observableFileSystemEntities;
        }
    }
}
