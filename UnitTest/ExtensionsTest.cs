using Microsoft.VisualStudio.TestTools.UnitTesting;
using Narumikazuchi;
using System;

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
        public void ThrowOnNull()
        {
            ExceptionHelpers.ThrowIfNullOrEmpty(source: "Test");
            Assert.ThrowsException<NullReferenceException>(() => ExceptionHelpers.ThrowIfNull(source: null));
        }

        [TestMethod]
        public void IsSingletonFalse()
        {
            Assert.IsFalse(typeof(Int64).IsSingleton());
        }

        [TestMethod]
        public void ExceptionHelperTest()
        {
            try
            {
                Exception exception = new(message: "Somethinng went wrong!");
                exception.Data.Add(key: "Typename",
                                   value: typeof(Int64).FullName);
                throw exception;
            }
            catch (Exception e)
            {
                var info = ExceptionHelpers.ExtractInformation(source: e);
                Assert.AreEqual(info.CallStack.Count, 1);
                Assert.AreEqual(info.CallStack[0].Line, 42);
                Assert.AreEqual(info.Data.Count, 1);
                Assert.AreEqual((String)info.Data["Typename"], typeof(Int64).FullName);
                Assert.AreEqual(info.SourceType, typeof(ExtensionsTest));
                Assert.AreEqual(info.SourceMemberType, "Method");
                Assert.AreEqual(info.SourceMember, "Void ExceptionHelperTest()");
                Assert.AreEqual(info.SourceLibrary, @"\\192.168.0.30\davy\Programmierung\Projects\Utility Common\UnitTest\bin\Debug\net6.0\UnitTest.dll");
            }
        }

        [TestMethod]
        public void SanitizeFilename()
        {
            String raw = @"\Some?F1l<n:m/>e";
            String sanitized = raw.SanitizeForFilename();
            Assert.AreEqual("SomeF1lnme", sanitized);
        }

        public TestContext TestContext
        {
            get => this._instance;
            set => this._instance = value;
        }

        private TestContext _instance;
    }
}
