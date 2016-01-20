﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using NUnit.Framework;

namespace FluentAssertions.Autofac
{
    public class ResolveAssertions<TService>
    {
        private readonly IContainer _container;
        private readonly List<TService> _instances;

        public ResolveAssertions(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            _container = container;
            _instances = _container.Resolve<IEnumerable<TService>>().ToList();
            if (!_instances.Any())
                throw new AssertionException($"Expected container to resolve '{typeof(TService)}'.");
        }

        public RegistrationAssertions As<TImplementation>()
            where TImplementation : TService
        {
            return As(typeof (TImplementation));
        }

        public RegistrationAssertions AsSelf()
        {
            return As<TService>();
        }

        public RegistrationAssertions As(Type type)
        {
            _instances.Should().Contain(instance => instance.GetType() == type,
                $"Type '{typeof (TService)}' should be resolved as '{type}'");

            return new RegistrationAssertions(_container, type);
        }
    }
}