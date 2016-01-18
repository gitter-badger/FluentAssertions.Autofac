﻿using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NEdifis.Attributes;
using NUnit.Framework;

namespace Autofac.TestingHelpers
{
    [TestFixtureFor(typeof(Module<>))]
    // ReSharper disable InconsistentNaming
    internal class Module_Should
    {
        [Test]
        public void Provide_test_container()
        {
            var container = Module<SampleModule>.GetTestContainer();
            container.Should().NotBeNull();
        }

        [Test]
        public void Provide_test_builder()
        {
            var builder = Module<SampleModule>.GetTestBuilder();
            builder.ShouldHave().RegisteredModule<SampleModule>();
        }

        [ExcludeFromCodeCoverage]
        private class SampleModule : Module { }
    }
}