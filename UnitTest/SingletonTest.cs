using Microsoft.VisualStudio.TestTools.UnitTesting;
using Narumikazuchi;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace UnitTest
{
    [TestClass]
    public partial class SingletonTest
    {
        [TestMethod]
        public void ValidSingletonClass()
        {
            ValidSingleton instance = Singleton<ValidSingleton>.Instance;
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void ReflectionSingleton()
        {
            _ = Singleton<ValidSingleton>.Instance;
            ConstructorInfo constructor = typeof(ValidSingleton).GetConstructor(bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic, 
                                                                                binder: null, 
                                                                                types: Array.Empty<Type>(), 
                                                                                modifiers: null);
            Assert.ThrowsException<TargetInvocationException>(() => constructor.Invoke(parameters: Array.Empty<Object>()));
        }

        [TestMethod]
        public void MultipleSingleton()
        {
            _ = Singleton<ValidSingleton>.Instance;
            FieldInfo field = typeof(Singleton).GetField(name: "_initialized", 
                                                         bindingAttr: BindingFlags.Static | BindingFlags.NonPublic);
            Collection<String> init = (Collection<String>)field.GetValue(obj: null);
            init.Add(typeof(ValidSingleton2).AssemblyQualifiedName);
            Assert.ThrowsException<TargetInvocationException>(() => _ = Singleton<ValidSingleton2>.Instance);
        }

        [TestMethod]
        public void AbstractClassSingleton()
        {
            Assert.ThrowsException<TypeInitializationException>(() => _ = Singleton<AbstractSingleton>.Instance);
        }

        [TestMethod]
        public void PublicConstructorSingleton()
        {
            Assert.ThrowsException<TypeInitializationException>(() => _ = Singleton<PublicSingleton>.Instance);
        }

        [TestMethod]
        public void NonPublicConstructorSingleton()
        {
            Assert.ThrowsException<ConstructorNotFound>(() => _ = Singleton<NonPublicFailedSingleton>.Instance);
        }
    }

    partial class SingletonTest
    {
        public class ValidSingleton : Singleton
        {
            private ValidSingleton() { }
        }

        public class ValidSingleton2 : Singleton
        {
            private ValidSingleton2() { }
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
}
