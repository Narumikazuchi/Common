using Narumikazuchi;

namespace Extensions;

[TestClass]
public sealed class A_string_during_sanitization
{
    [TestClass]
    public sealed class will_throw_argument_null_exception
    {
        [TestMethod]
        public void if_it_is_null()
        {
            String? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => value!.SanitizeForFilename());
        }
    }

    [TestClass]
    public sealed class will_return
    {
        [TestMethod]
        public void a_sanitized_string_if_it_contains_invalid_characters()
        {
            String value = "F?le.b:n";
            String expected = "Fle.bn";
            if (OperatingSystem.IsWindows())
            {
                Assert.AreEqual(expected, value.SanitizeForFilename());
            }
            else if (OperatingSystem.IsLinux())
            {
                Assert.AreEqual(value, value.SanitizeForFilename());
            }
        }

        [TestMethod]
        public void the_same_string_if_it_does_not_contain_invalid_characters()
        {
            String value = "File.bin";
            if (OperatingSystem.IsWindows())
            {
                Assert.AreEqual(value, value.SanitizeForFilename());
            }
            else if (OperatingSystem.IsLinux())
            {
                Assert.AreEqual(value, value.SanitizeForFilename());
            }
        }
    }
}