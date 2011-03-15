using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using System.IO;
using System.Reactive;
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
        public void FluentApiWorks()
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
                .WithCommand<FilesOnly>()
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
                .WithCommand<FilesOnly>()
                .WithCommand(new KeepLastMonth())
                .CleanUp("test");

            blockbuster.AssertWasCalled(x => x.CleanUp(Arg<string>.Is.Equal("test"),
                Arg<IEnumerable<AbstractCommand>>.Matches(List.Element(0, Is.TypeOf<FilesOnly>()) && List.Element(1, Is.TypeOf<KeepLastMonth>()))));
        }

    }
}


