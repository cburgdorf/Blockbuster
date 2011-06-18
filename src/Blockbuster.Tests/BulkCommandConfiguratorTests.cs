using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Configuration;
using NUnit.Framework;

namespace Blockbuster.Tests
{
    [TestFixture]
    public class BulkCommandConfiguratorTests
    {
        [Test]
        public void ReturnsBulkCommandInfoWithOneDirectoryAndOneCommand()
        {
            var configurationString = "CommandName='FilesOnly'; Directory='C:\\Test\\'";
            var bulkCommandConfigurator = new BulkCommandConfigurator();
            var bulkCommandInfo = bulkCommandConfigurator.ParseCommandStrings(new[] {configurationString});
            
            Assert.AreEqual(1, bulkCommandInfo.InvocationInfos.Count());
            var invocationInfo = bulkCommandInfo.InvocationInfos.First();
            Assert.AreEqual("filesonly", invocationInfo.Value[0].CommandName);
            Assert.AreEqual("c:\\test\\", invocationInfo.Key);
            Assert.That(invocationInfo.Value[0].Configuration.Count == 0, "Contains configuration wheras it should not");
        }

        [Test]
        public void ReturnsBulkCommandInfoWithOneDirectoryAndTwoCommands()
        {
            var configurationString = "CommandName='FilesOnly'; Directory='C:\\Test\\'";
            var configurationString2 = "CommandName='FileExtension'; Directory='C:\\Test\\'; FileExtension='txt'";
            var bulkCommandConfigurator = new BulkCommandConfigurator();
            var bulkCommandInfo = bulkCommandConfigurator.ParseCommandStrings(new[] { configurationString, configurationString2 });
            Assert.AreEqual(1, bulkCommandInfo.InvocationInfos.Count());
            var invocationInfoFirst = bulkCommandInfo.InvocationInfos.First();
            Assert.AreEqual("c:\\test\\", invocationInfoFirst.Key);
            Assert.AreEqual("filesonly", invocationInfoFirst.Value[0].CommandName);
            Assert.AreEqual("fileextension", invocationInfoFirst.Value[1].CommandName);
            Assert.AreEqual("txt", invocationInfoFirst.Value[1].Configuration["fileextension"]);
            Assert.That(invocationInfoFirst.Value[1].Configuration.Count == 1, "Contains uncorrect number of configuration properties");
        }

        [Test]
        public void ReturnsBulkCommandInfoWithTwoDirectoriesAndThreeCommands()
        {
            var configurationString = "CommandName='FilesOnly'; Directory='C:\\Test\\'";
            var configurationString2 = "CommandName='FileExtension'; Directory='C:\\Test\\'; FileExtension='txt'";
            var configurationString3 = "CommandName='FileExtension'; Directory='C:\\Test\\Test\\'; FileExtension='pdf'";
            
            var bulkCommandConfigurator = new BulkCommandConfigurator();
            var bulkCommandInfo = bulkCommandConfigurator.ParseCommandStrings(new[] { configurationString, configurationString2, configurationString3 });
            Assert.AreEqual(2, bulkCommandInfo.InvocationInfos.Count());
            var invocationInfoFirst = bulkCommandInfo.InvocationInfos.First();
            Assert.That(invocationInfoFirst.Value.Count == 2, "Contains incorrect count of commands for first directory");
            Assert.AreEqual("c:\\test\\", invocationInfoFirst.Key);
            Assert.AreEqual("filesonly", invocationInfoFirst.Value[0].CommandName);
            Assert.AreEqual("fileextension", invocationInfoFirst.Value[1].CommandName);
            Assert.AreEqual("txt", invocationInfoFirst.Value[1].Configuration["fileextension"]);
            Assert.That(invocationInfoFirst.Value[1].Configuration.Count == 1, "Contains uncorrect number of configuration properties");

            var invocationInfoSecond = bulkCommandInfo.InvocationInfos.Skip(1).First();
            Assert.That(invocationInfoSecond.Value.Count == 1, "Contains incorrect count of commands for first directory");
            Assert.AreEqual("c:\\test\\test\\", invocationInfoSecond.Key);
            Assert.AreEqual("fileextension", invocationInfoSecond.Value[0].CommandName);
            Assert.AreEqual("pdf", invocationInfoSecond.Value[0].Configuration["fileextension"]);
            Assert.That(invocationInfoSecond.Value[0].Configuration.Count == 1, "Contains uncorrect number of configuration properties");
        
        }
    }
}
