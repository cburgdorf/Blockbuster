using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Blockbuster.DirectoryIterator;
using Blockbuster.Core;
using Blockbuster.Commands;
using Blockbuster.Commands.Filtering;
using Blockbuster.Fluent;
using Blockbuster.Contracts;

namespace Blockbuster.Tests
{
    [NUnit.Framework.TestFixture]
    public class FluentActivatorTests
    {
        [NUnit.Framework.Test]
        public void FluentApiWithGenericRegistrationWorks()
        {
            var blockbuster = Rhino.Mocks.MockRepository.GenerateStub<IBlockbuster>();
                blockbuster
                    .WithCommand<FilesOnly>()
                    .WithCommand<KeepLastMonth>()
                    .CleanUp("test");

                blockbuster.AssertWasCalled(x => x.CleanUp(Arg<string>.Is.Equal("test"),
                    Arg<IEnumerable<AbstractCommand>>.Matches(List.Element(0, Is.TypeOf<FilesOnly>()) && List.Element(1, Is.TypeOf<KeepLastMonth>()))));
        }

        [NUnit.Framework.Test]
        public void FluentApiWithFuncRegistrationWorks()
        {
            var blockbuster = Rhino.Mocks.MockRepository.GenerateStub<IBlockbuster>();
            blockbuster
				.WithCommand(() => new FilesOnly())
				.WithCommand(() => new KeepLastMonth())				
                .CleanUp("test");

            blockbuster.AssertWasCalled(x => x.CleanUp(Arg<string>.Is.Equal("test"),
                Arg<IEnumerable<AbstractCommand>>.Matches(List.Element(0, Is.TypeOf<FilesOnly>()) && List.Element(1, Is.TypeOf<KeepLastMonth>()))));
        }

        [NUnit.Framework.Test]
        public void FluentApiWithObjectRegistrationWorks()
        {
            var blockbuster = Rhino.Mocks.MockRepository.GenerateStub<IBlockbuster>();
            blockbuster            
				.WithCommand(new FilesOnly())
				.WithCommand(new KeepLastMonth())
				.CleanUp("test");

            blockbuster.AssertWasCalled(x => x.CleanUp(Arg<string>.Is.Equal("test"),
                Arg<IEnumerable<AbstractCommand>>.Matches(List.Element(0, Is.TypeOf<FilesOnly>()) && List.Element(1, Is.TypeOf<KeepLastMonth>()))));
        }

		//Might be helpful as long as the arguments differ
		[NUnit.Framework.Test]
		public void FluentApiKeepsDuplicateCommands()
		{
			var blockbuster = Rhino.Mocks.MockRepository.GenerateStub<IBlockbuster>();
			blockbuster
				.WithCommand<FilesOnly>()
				.WithCommand<FilesOnly>()
				.CleanUp("test");

			blockbuster.AssertWasCalled(x => x.CleanUp(Arg<string>.Is.Equal("test"),
				Arg<IEnumerable<AbstractCommand>>.Matches(List.Element(0, Is.TypeOf<FilesOnly>()) && List.Element(1, Is.TypeOf<FilesOnly>()))));
		}

		[NUnit.Framework.Test]
		public void FluentApiWithFuncRegistrationKeepsCommandParameters()
		{
			var blockbuster = Rhino.Mocks.MockRepository.GenerateStub<IBlockbuster>();
			blockbuster
				.WithCommand(() => new FileExtension("txt"))
				.CleanUp("test");

			blockbuster.AssertWasCalled(x => x.CleanUp(Arg<string>.Is.Equal("test"),
				Arg<IEnumerable<AbstractCommand>>.Matches(List.Element(0, Is.TypeOf<FileExtension>()) && List.Element(0, Property.Value("AdditionalParameters","txt")))));
		}

		[NUnit.Framework.Test]
		public void FluentApiWithInstanceRegistrationKeepsCommandParameters()
		{
			var blockbuster = Rhino.Mocks.MockRepository.GenerateStub<IBlockbuster>();
			blockbuster
				.WithCommand(new FileExtension("txt"))
				.CleanUp("test");

			blockbuster.AssertWasCalled(x => x.CleanUp(Arg<string>.Is.Equal("test"),
				Arg<IEnumerable<AbstractCommand>>.Matches(List.Element(0, Is.TypeOf<FileExtension>()) && List.Element(0, Property.Value("AdditionalParameters", "txt")))));
		}
    }
}


