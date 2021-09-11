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
        public void IsSingletonTrue()
        {
            Assert.IsTrue(typeof(ValidSingleton).IsSingleton());
        }

        [TestMethod]
        public void IsSingletonFalse()
        {
            Assert.IsFalse(typeof(Int64).IsSingleton());
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
