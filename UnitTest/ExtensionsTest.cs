using Microsoft.VisualStudio.TestTools.UnitTesting;
using Narumikazuchi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitTest
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void Clamp()
        {
            Int32 result = 72.Clamp(32, 64);
            Assert.AreEqual(64, result);
            result = 72.Clamp(32, 128);
            Assert.AreEqual(72, result);
            result = 72.Clamp(128, 256);
            Assert.AreEqual(128, result);
        }

        [TestMethod]
        public void DeepCopy()
        {
            PropertyInfo property = typeof(IndexerSignature).GetProperty(nameof(IndexerSignature.Indecies));
            IndexerSignature original = new(property);
            IndexerSignature copy = original.DeepCopy();
            Assert.IsTrue(original.Equals(copy));
        }

        [TestMethod]
        public void IsSingletonTrue()
        {
            Assert.IsTrue(typeof(ValidSingleton).IsSingleton());
        }

        [TestMethod]
        public void IsSingletonFalse()
        {
            Assert.IsFalse(typeof(IndexerSignature).IsSingleton());
        }
    }

    public readonly partial struct IndexerSignature
    {
        internal IndexerSignature(PropertyInfo indexer)
        {
            this.Type = indexer.PropertyType;
            this.Indecies = new List<Type>(indexer.GetIndexParameters().Select(p => p.ParameterType));
        }

        public Type Type { get; }
        public IReadOnlyList<Type> Indecies { get; }
    }

    partial struct IndexerSignature : IEquatable<IndexerSignature>
    {
        public Boolean Equals(IndexerSignature other) =>
            this.Type == other.Type &&
            this.Indecies.SequenceEqual(other.Indecies);

        public override Boolean Equals(Object obj) =>
            obj is IndexerSignature other &&
            this.Equals(other);

        public override Int32 GetHashCode() =>
            this.Type.GetHashCode() ^
            this.Indecies.GetHashCode();

        public static Boolean operator ==(IndexerSignature left, IndexerSignature right) =>
            left.Equals(right);
        public static Boolean operator !=(IndexerSignature left, IndexerSignature right) =>
            !left.Equals(right);
    }
}
