using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Blockbuster.Tests.TestFileGenerator
{
    public static class Generator
    {
        public static void Generate(string directory, int level, int filesPerLevel)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            directoryInfo.Create();
            CreateTestFiles(directoryInfo.FullName, filesPerLevel, "test_", ".txt");                       

            if (level == 0)
                return;
            else
            {
                string subDirectory = GetSubDirectoryName(directoryInfo.FullName);
                level--;
                Generate(subDirectory, level, filesPerLevel);
            }
        }

        private static void CreateTestFiles(string baseDirectory, int amount, string prefix, string suffix)
        {
            for (int i=0;i<amount;i++)
            {
                string fullFileName = string.Format("{0}\\{1}{2}{3}", baseDirectory, prefix, Guid.NewGuid().ToString(),suffix);
                File.Create(fullFileName);
            }
        }

        private static string GetSubDirectoryName(string directory)
        {
            string subDirectoryName = Guid.NewGuid().ToString();
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            DirectoryInfo subDirectoryInfo = new DirectoryInfo(directoryInfo.FullName + "\\" + subDirectoryName);
            return subDirectoryInfo.FullName;
        }
    }
}
