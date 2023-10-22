using Narumikazuchi;

namespace Exceptions;

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
            Assert.ThrowsException<ArgumentNullException>(() => value.ThrowIfLesserThan("Test"));
        }

        [TestMethod]
        public void if_lesser_than_boundary_is_null()
        {
            String? value = String.Empty;
            Assert.ThrowsException<ArgumentNullException>(() => value.ThrowIfLesserThan(null!));
        }

        [TestMethod]
        public void if_lower_boundary_is_null()
        {
            String? value = String.Empty;
            Assert.ThrowsException<ArgumentNullException>(() => value.ThrowIfOutOfRange(null!, "Test"));
        }

        [TestMethod]
        public void if_bigger_than_boundary_is_null()
        {
            String? value = String.Empty;
            Assert.ThrowsException<ArgumentNullException>(() => value.ThrowIfBiggerThan(null!));
        }

        [TestMethod]
        public void if_upper_boundary_is_null()
        {
            String? value = String.Empty;
            Assert.ThrowsException<ArgumentNullException>(() => value.ThrowIfOutOfRange("Test", null!));
        }
    }

    [TestClass]
    public sealed class will_throw_argument_out_of_range_exception
    {
        [TestMethod]
        public void if_is_lesser_than_boundary()
        {
            Int32 value = 10;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => value.ThrowIfLesserThan(15));
        }

        [TestMethod]
        public void if_is_bigger_than_boundary()
        {
            Int32 value = 10;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => value.ThrowIfBiggerThan(5));
        }

        [TestMethod]
        public void if_is_lesser_than_low_boundary()
        {
            Int32 value = 10;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => value.ThrowIfOutOfRange(15, 30));
        }

        [TestMethod]
        public void if_is_bigger_than_high_boundary()
        {
            Int32 value = 10;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => value.ThrowIfOutOfRange(0, 5));
        }
    }

    [TestClass]
    public sealed class will_not_throw
    {
        [TestMethod]
        public void if_is_equal_to_lesser_boundary()
        {
            Int32 value = 10;
            value.ThrowIfLesserThan(10);
        }

        [TestMethod]
        public void if_is_bigger_than_lesser_boundary()
        {
            Int32 value = 10;
            value.ThrowIfLesserThan(5);
        }

        [TestMethod]
        public void if_is_equal_to_bigger_boundary()
        {
            Int32 value = 10;
            value.ThrowIfBiggerThan(10);
        }

        [TestMethod]
        public void if_is_lesser_than_bigger_boundary()
        {
            Int32 value = 10;
            value.ThrowIfBiggerThan(15);
        }

        [TestMethod]
        public void if_is_equal_to_low_boundary()
        {
            Int32 value = 10;
            value.ThrowIfOutOfRange(10, 30);
        }

        [TestMethod]
        public void if_is_equal_to_high_boundary()
        {
            Int32 value = 10;
            value.ThrowIfOutOfRange(0, 10);
        }

        [TestMethod]
        public void if_is_bigger_than_low_boundary_and_lesser_than_high_boundary()
        {
            Int32 value = 10;
            value.ThrowIfOutOfRange(0, 20);
        }
    }
}