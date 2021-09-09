using Microsoft.VisualStudio.TestTools.UnitTesting;
using Narumikazuchi;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace UnitTest
{
    [TestClass]
    public class SingletonTest
    {
        [TestMethod]
        public void ValidSingleton()
        {
            ValidSingleton instance = Singleton<ValidSingleton>.Instance;
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void ReflectionSingleton()
        {
            ValidSingleton instance = Singleton<ValidSingleton>.Instance;
            ConstructorInfo constructor = typeof(ValidSingleton).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
            constructor.Invoke(Array.Empty<Object>());
        }

        [TestMethod]
        public void MultipleSingleton()
        {
            FieldInfo field = typeof(Singleton).GetField("_initialized");
            Collection<String> init = (Collection<String>)field.GetValue(null);
            init.Add(typeof(ValidSingleton).AssemblyQualifiedName);
            ValidSingleton instance = Singleton<ValidSingleton>.Instance;
        }

        [TestMethod]
        public void AbstractSingleton()
        {
            AbstractSingleton instance = Singleton<AbstractSingleton>.Instance;
        }

        [TestMethod]
        public void PublicSingleton()
        {
            PublicSingleton instance = Singleton<PublicSingleton>.Instance;
        }

        [TestMethod]
        public void NonPublicSingleton()
        {
            NonPublicFailedSingleton instance = Singleton<NonPublicFailedSingleton>.Instance;
        }
    }

    public class ValidSingleton : Singleton
    {
        private ValidSingleton() { }
    }

    public abstract class AbstractSingleton : Singleton
    {
        private AbstractSingleton() { }
    }

    public class PublicSingleton : Singleton
    {
        public PublicSingleton() { }
    }

    public class NonPublicFailedSingleton : Singleton
    {
        private NonPublicFailedSingleton(String _) { }
    }
}
