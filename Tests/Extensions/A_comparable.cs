using Narumikazuchi;

namespace Extensions;

[TestClass]
#pragma warning disable CS8631
public sealed class A_comparable
{
    [TestClass]
    public sealed class will_throw_argument_null_exception
    {
        [TestMethod]
        public void if_null()
        {
            String? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => value.Clamp("Lower", "Higher"));
        }
    }

    [TestClass]
    public sealed class will_return_low_boundary
    {
        [TestMethod]
        public void if_is_equal_to_boundary()
        {
            Int32 value = 10;
            Assert.AreEqual(value.Clamp(10, 30), 10);
        }

        [TestMethod]
        public void if_is_lesser_than_boundary()
        {
            Int32 value = 10;
            Assert.AreEqual(value.Clamp(20, 30), 20);
        }
    }

    [TestClass]
    public sealed class will_return_high_boundary
    {
        [TestMethod]
        public void if_is_equal_to_boundary()
        {
            Int32 value = 10;
            Assert.AreEqual(value.Clamp(0, 10), 10);
        }

        [TestMethod]
        public void if_is_bigger_than_boundary()
        {
            Int32 value = 10;
            Assert.AreEqual(value.Clamp(0, 5), 5);
        }
    }

    [TestClass]
    public sealed class will_return_clamped_value
    {
        [TestMethod]
        public void if_is_between_lower_and_upper_boundary()
        {
            Int32 value = 10;
            Assert.AreEqual(value.Clamp(0, 20), 10);
        }
    }
}