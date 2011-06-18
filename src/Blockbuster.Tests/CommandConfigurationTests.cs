using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Configuration;
using NUnit.Framework;

namespace Blockbuster.Tests
{
    [TestFixture]
    public class CommandConfigurationTests
    {
        [Test]
        public void ReturnsEmptyDictionaryForEmptyString()
        {
            var configurationString = "";
            var commandConfiguration = new CommandConfiguration(configurationString);
            Assert.IsNotNull(commandConfiguration.Configuration, "Dictonary expected, but is null");
        }

        [Test]
        public void SplitsFragmentsWithApostrophs()
        {
            var configurationString = "CommandName='FilesOnly'; Directory='C:\\Test\\'";
            var commandConfiguration = new CommandConfiguration(configurationString);
            Assert.That(commandConfiguration.Configuration.ContainsKey("commandname"), "Missing Key: filesonly");
            Assert.That(commandConfiguration.Configuration.ContainsKey("directory"), "Missing Key: directory");
            Assert.AreEqual("filesonly", commandConfiguration.Configuration["commandname"], "Wrong Command Name");
            Assert.AreEqual("c:\\test\\", commandConfiguration.Configuration["directory"], "Wrong Command Name");
        }

        [Test]
        public void SplitsFragmentsWithApostrophsAndThreeItems()
        {
            var configurationString = "CommandName='FileExtension'; Directory='C:\\Test\\'; FileExtension='txt'";
            var commandConfiguration = new CommandConfiguration(configurationString);
            Assert.That(commandConfiguration.Configuration.ContainsKey("commandname"), "Missing Key: fileextension");
            Assert.That(commandConfiguration.Configuration.ContainsKey("directory"), "Missing Key: directory");
            Assert.That(commandConfiguration.Configuration.ContainsKey("fileextension"), "Missing Key: fileextension");
            Assert.AreEqual("fileextension", commandConfiguration.Configuration["commandname"], "Wrong Command Name");
            Assert.AreEqual("c:\\test\\", commandConfiguration.Configuration["directory"], "Wrong Command Name");
            Assert.AreEqual("txt", commandConfiguration.Configuration["fileextension"], "Wrong FileExtension");
        }

        [Test]
        public void SplitsFragmentsWithoutApostrophs()
        {
            var configurationString = "CommandName=FilesOnly; Directory=C:\\Test\\";
            var commandConfiguration = new CommandConfiguration(configurationString);
            Assert.That(commandConfiguration.Configuration.ContainsKey("commandname"), "Missing Key: filesonly");
            Assert.That(commandConfiguration.Configuration.ContainsKey("directory"), "Missing Key: directory");
            Assert.AreEqual("filesonly", commandConfiguration.Configuration["commandname"], "Wrong Command Name");
            Assert.AreEqual("c:\\test\\", commandConfiguration.Configuration["directory"], "Wrong Command Name");
        }

        [Test]
        public void SplitsFragmentsWithoutSpacings()
        {
            var configurationString = "CommandName= FilesOnly ; Directory= C:\\Test\\ ";
            var commandConfiguration = new CommandConfiguration(configurationString);
            Assert.That(commandConfiguration.Configuration.ContainsKey("commandname"), "Missing Key: filesonly");
            Assert.That(commandConfiguration.Configuration.ContainsKey("directory"), "Missing Key: directory");
            Assert.AreEqual("filesonly", commandConfiguration.Configuration["commandname"], "Wrong Command Name");
            Assert.AreEqual("c:\\test\\", commandConfiguration.Configuration["directory"], "Wrong Command Name");
        }
    }
}
