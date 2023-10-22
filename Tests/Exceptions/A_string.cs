using Narumikazuchi;

namespace Exceptions;

[TestClass]
public sealed class A_string
{
    [TestClass]
    public sealed class will_throw_argument_null_exception
    {
        [TestMethod]
        public void if_null()
        {
            String? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => value.ThrowIfNullOrEmpty(asArgumentException: true));
        }

        [TestMethod]
        public void if_empty()
        {
            String? value = String.Empty;
            Assert.ThrowsException<ArgumentNullException>(() => value.ThrowIfNullOrEmpty(asArgumentException: true));
        }

        [TestMethod]
        public void if_whitespace()
        {
            String? value = "\t";
            Assert.ThrowsException<ArgumentNullException>(() => value.ThrowIfNullOrEmpty(asArgumentException: true));
        }
    }

    [TestClass]
    public sealed class will_throw_null_reference_exception
    {
        [TestMethod]
        public void if_null()
        {
            String? value = default;
            Assert.ThrowsException<NullReferenceException>(() => value.ThrowIfNullOrEmpty());
        }

        [TestMethod]
        public void if_empty()
        {
            String? value = String.Empty;
            Assert.ThrowsException<NullReferenceException>(() => value.ThrowIfNullOrEmpty());
        }

        [TestMethod]
        public void if_whitespace()
        {
            String? value = "\t";
            Assert.ThrowsException<NullReferenceException>(() => value.ThrowIfNullOrEmpty());
        }
    }

    [TestClass]
    public sealed class will_not_throw
    {
        [TestMethod]
        public void if_whitespace_padded_left()
        {
            String? value = "\tTest";
            value.ThrowIfNullOrEmpty();
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void if_whitespace_padded_right()
        {
            String? value = "Test\t";
            value.ThrowIfNullOrEmpty();
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void if_non_empty()
        {
            String? value = "Test";
            value.ThrowIfNullOrEmpty();
            Assert.IsNotNull(value);
        }
    }
}