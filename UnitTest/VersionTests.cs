using Microsoft.VisualStudio.TestTools.UnitTesting;
using Narumikazuchi;

namespace UnitTest
{
    [TestClass]
    public class VersionTests
    {
        [TestMethod]
        public void CreateInstanceTest()
        {
            AlphanumericVersion version = new();
            String value = version.ToString();
            _instance.WriteLine(value);
            Assert.AreEqual(value, "0");
            version = new(1, 0, 0);
            value = version.ToString();
            _instance.WriteLine(value);
            Assert.AreEqual(value, "1.0.0");
            version = new(1, 2, "beta3");
            value = version.ToString("#.#-#");
            _instance.WriteLine(value);
            Assert.AreEqual(value, "1.2-beta3");
            version = new("0xEB", "0x23", "revamp16");
            value = version.ToString("#.#-#");
            _instance.WriteLine(value);
            Assert.AreEqual(value, "0xEB.0x23-revamp16");
            Assert.ThrowsException<ArgumentException>(() => new AlphanumericVersion("13€", "12$"));
            Assert.ThrowsException<ArgumentNullException>(() => new AlphanumericVersion(5, null, 3));
        }

        [TestMethod]
        public void CloneTest()
        {
            AlphanumericVersion original = new(1, 6, "alpha");
            AlphanumericVersion clone = (AlphanumericVersion)original.Clone();
            Assert.AreEqual(original, clone);
        }

        [TestMethod]
        public void ComparableTest()
        {
            AlphanumericVersion original = new(1, 6, "alpha");
            AlphanumericVersion other = new(2, 0, 0);
            Object nothing = null;
            Assert.AreEqual(original.CompareTo(other), -1);
            Assert.AreEqual(((IComparable)original).CompareTo(nothing), 1);
        }

        [TestMethod]
        public void EqualityTest()
        {
            AlphanumericVersion original = new(1, 6, "alpha");
            AlphanumericVersion clone = (AlphanumericVersion)original.Clone();
            AlphanumericVersion other = new(2, 0, 0);
            Assert.IsTrue(original == clone);
            Assert.IsTrue(original.Equals(other: clone));
            Assert.IsFalse(original == other);
            Assert.IsFalse(original.Equals(other: other));
        }

        [TestMethod]
        public void ParseableTest()
        {
            String text = "1.0.0";
            AlphanumericVersion version = AlphanumericVersion.Parse(text);
            Assert.AreEqual(version, new(1, 0, 0));
            Assert.IsTrue(AlphanumericVersion.TryParse(text, out version));
            text = "...";
            Assert.ThrowsException<ArgumentException>(() => AlphanumericVersion.Parse(text));
            Assert.IsFalse(AlphanumericVersion.TryParse(text, out version));
            text = "1.1.1.1.1.1";
            Assert.ThrowsException<ArgumentException>(() => AlphanumericVersion.Parse(text));
            Assert.IsFalse(AlphanumericVersion.TryParse(text, out version));
            text = "€uro.12.55316";
            Assert.ThrowsException<FormatException>(() => AlphanumericVersion.Parse(text));
            Assert.IsFalse(AlphanumericVersion.TryParse(text, out version));
            text = "3.0alpha6";
            version = AlphanumericVersion.Parse(text);
            Assert.AreEqual(version, new(3, "0alpha6"));
            Assert.IsTrue(AlphanumericVersion.TryParse(text, out version));
            text = "4-3-55643";
            version = AlphanumericVersion.Parse(text);
            Assert.AreEqual(version, new("4-3-55643"));
            Assert.IsTrue(AlphanumericVersion.TryParse(text, out version));
        }

        public TestContext TestContext
        {
            get => _instance;
            set => _instance = value;
        }

        private static TestContext _instance;
    }
}
