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
        public void Prime()
        {
            _instance.WriteLine(Primes.GetNext(15).ToString());
            _instance.WriteLine(Primes.GetNext(999).ToString());
            _instance.WriteLine(Primes.GetPrevious(21).ToString());
            _instance.WriteLine(Primes.GetPrevious(1015).ToString());
            _instance.WriteLine(Primes.IsPrime(883).ToString());
            _instance.WriteLine(Primes.IsPrime(88).ToString());
        }

        [TestMethod]
        public void ThrowOnNull()
        {
            ExceptionHelpers.ThrowIfNull(source: "Test");
            Assert.ThrowsException<NullReferenceException>(() => ExceptionHelpers.ThrowIfNull(source: null));
        }

        [TestMethod]
        public void ExceptionHelperTest()
        {
            try
            {
                Exception exception = new(message: "Something went wrong!");
                exception.Data.Add(key: "Typename",
                                   value: typeof(Int64).FullName);
                throw exception;
            }
            catch (Exception e)
            {
                ExceptionInformation info = ExceptionHelpers.ExtractInformation(source: e);
                Assert.AreEqual(info.CallStack.Count, 1);
                Assert.AreEqual(info.CallStack[0].Line, 47);
                Assert.AreEqual(info.Data.Count, 1);
                Assert.AreEqual((String)info.Data["Typename"], typeof(Int64).FullName);
                Assert.AreEqual(info.SourceType, typeof(ExtensionsTest));
                Assert.AreEqual(info.SourceMemberType, "Method");
                Assert.AreEqual(info.SourceMember, "Void ExceptionHelperTest()");
                Assert.AreEqual(info.SourceLibrary, @"D:\Data\Software Development\Projects\Utility Common\UnitTest\bin\Debug\net6.0\UnitTest.dll");
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
