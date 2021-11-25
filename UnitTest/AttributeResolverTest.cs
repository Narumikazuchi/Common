﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Narumikazuchi;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Narumikazuchi")]

namespace UnitTest
{
    [Single]
    [Multi]
    [Multi]
    [TestClass]
    public class AttributeResolverTest
    {
        [TestMethod]
        public void AssemblyHasAttributeTrue()
        {
            Boolean result = AttributeResolver.HasAttribute<InternalsVisibleToAttribute>(assembly: Assembly.GetExecutingAssembly());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AssemblyHasAttributeFalse()
        {
            Boolean result = AttributeResolver.HasAttribute<UnusedAttribute>(assembly: Assembly.GetExecutingAssembly());
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AssemblyFetchAttributes()
        {
            IEnumerable<InternalsVisibleToAttribute> values = AttributeResolver.FetchAllAttributes<InternalsVisibleToAttribute>(assembly: Assembly.GetExecutingAssembly());
            Assert.IsTrue(values.Count() == 1);
        }

        [TestMethod]
        public void AssemblyFetchOnlyAllowedAttribute()
        {
            Assert.ThrowsException<NotAllowed>(() => AttributeResolver.FetchOnlyAllowedAttribute<InternalsVisibleToAttribute>(assembly: Assembly.GetExecutingAssembly()));
        }

        [TestMethod]
        public void MemberHasAttributeTrue()
        {
            Boolean result = AttributeResolver.HasAttribute<SingleAttribute>(info: typeof(AttributeResolverTest));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MemberHasAttributeFalse()
        {
            Boolean result = AttributeResolver.HasAttribute<UnusedAttribute>(info: typeof(AttributeResolverTest));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void MemberFetchAttributes()
        {
            IEnumerable<MultiAttribute> values = AttributeResolver.FetchAllAttributes<MultiAttribute>(info: typeof(AttributeResolverTest));
            Assert.IsTrue(values.Count() == 2);
        }

        [TestMethod]
        public void MemberFetchOnlyAllowedAttribute()
        {
            SingleAttribute value = AttributeResolver.FetchOnlyAllowedAttribute<SingleAttribute>(info: typeof(AttributeResolverTest));
            Assert.IsNotNull(value);
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SingleAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MultiAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class UnusedAttribute : Attribute
    { }
}