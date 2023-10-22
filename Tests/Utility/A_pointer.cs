using Narumikazuchi;

namespace Utility;

[TestClass]
public sealed class A_pointer
{
    [TestClass]
    public sealed class as_default_struct
    {
        [TestMethod]
        public void will_not_throw_on_creation()
        {
            Pointer<Int32> pointer = default;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_in_array_creation()
        {
            Pointer<Int32>[] pointers = new Pointer<Int32>[3];
            Assert.IsNotNull(pointers);
        }

        [TestMethod]
        public void can_be_cast_to_a_signed_managed_pointer()
        {
            Pointer<Int32> pointer = default;
            IntPtr managed = pointer;
            Assert.AreEqual(managed, IntPtr.Zero);
        }

        [TestMethod]
        public void can_be_cast_to_a_unsigned_managed_pointer()
        {
            Pointer<Int32> pointer = default;
            UIntPtr managed = pointer;
            Assert.AreEqual(managed, UIntPtr.Zero);
        }

        [TestMethod]
        public unsafe void can_be_cast_to_a_unmanaged_pointer()
        {
            Pointer<Int32> pointer = default;
            void* unmanaged = pointer;
            Assert.AreEqual((Int64)unmanaged, 0L);
        }

        [TestMethod]
        public void will_not_throw_on_post_increment()
        {
            Pointer<Int32> pointer = default;
            pointer = pointer++;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_assign_increment()
        {
            Pointer<Int32> pointer = default;
            pointer += 42;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_post_decrement()
        {
            Pointer<Int32> pointer = default;
            pointer = pointer--;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_assign_decrement()
        {
            Pointer<Int32> pointer = default;
            pointer -= 42;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_return_null_pointer_for_address()
        {
            Pointer<Int32> pointer = default;
            Assert.AreEqual(pointer.Address, UIntPtr.Zero);
        }

        [TestMethod]
        public void will_throw_a_null_reference_exception_for_to_string()
        {
            Pointer<Int32> pointer = default;
            Assert.ThrowsException<NullReferenceException>(pointer.ToString);
        }

        [TestMethod]
        public void will_throw_a_null_reference_exception_for_indexer_getter()
        {
            Pointer<Int32> pointer = default;
            Assert.ThrowsException<NullReferenceException>(() => pointer[0]);
        }

        [TestMethod]
        public void will_throw_a_null_reference_exception_for_indexer_setter()
        {
            Pointer<Int32> pointer = default;
            Assert.ThrowsException<NullReferenceException>(() => pointer[0]);
        }

        [TestMethod]
        public void will_throw_a_null_reference_exception_for_value_getter()
        {
            Pointer<Int32> pointer = default;
            Assert.ThrowsException<NullReferenceException>(() => pointer.Value);
        }

        [TestMethod]
        public void will_throw_a_null_reference_exception_for_value_setter()
        {
            Pointer<Int32> pointer = default;
            Assert.ThrowsException<NullReferenceException>(() => pointer.Value = 42);
        }
    }

    [TestClass]
    public sealed class as_value_type_pointer
    {
        [TestMethod]
        public void will_not_throw_on_creation()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void can_be_cast_to_signed_managed_pointer()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            IntPtr managed = pointer;
            Assert.AreNotEqual(managed, IntPtr.Zero);
        }

        [TestMethod]
        public void can_be_cast_to_unsigned_managed_pointer()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            UIntPtr managed = pointer;
            Assert.AreNotEqual(managed, UIntPtr.Zero);
        }

        [TestMethod]
        public unsafe void can_be_cast_to_unmanaged_pointer()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            void* unmanaged = pointer;
            Assert.AreNotEqual((Int64)unmanaged, 0L);
        }

        [TestMethod]
        public void will_not_throw_on_post_increment()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            pointer = pointer++;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_assign_increment()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            pointer += 42;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_post_decrement()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            pointer = pointer--;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_assign_decrement()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            pointer -= 42;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_return_null_pointer_for_address()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            Assert.AreNotEqual(pointer.Address, UIntPtr.Zero);
        }

        [TestMethod]
        public void will_return_value_for_to_string()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            Assert.AreEqual(pointer.ToString(), "42");
        }

        [TestMethod]
        public void will_return_value_for_indexer_getter()
        {
            Span<Int32> values = stackalloc Int32[] { 42, 69 };
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref values[0]);
            Assert.AreEqual(pointer[0], 42);
            Assert.AreEqual(pointer[1], 69);
        }

        [TestMethod]
        public void will_update_value_for_indexer_setter()
        {
            Span<Int32> values = stackalloc Int32[] { 42, 69 };
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref values[0]);
            pointer[0] = 420;
            Assert.AreEqual(pointer[0], 420);
        }

        [TestMethod]
        public void will_return_value_for_value_getter()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            Assert.AreEqual(pointer.Value, 42);
        }

        [TestMethod]
        public void will_update_value_for_value_setter()
        {
            Int32 value = 42;
            Pointer<Int32> pointer = Pointer<Int32>.AddressOf(ref value);
            pointer.Value = 420;
            Assert.AreEqual(pointer.Value, 420);
        }
    }

    [TestClass]
    public sealed class as_reference_type_pointer
    {
        [TestMethod]
        public void will_not_throw_on_creation()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void can_be_cast_to_signed_managed_pointer()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            IntPtr managed = pointer;
            Assert.AreNotEqual(managed, IntPtr.Zero);
        }

        [TestMethod]
        public void can_be_cast_to_unsigned_managed_pointer()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            UIntPtr managed = pointer;
            Assert.AreNotEqual(managed, UIntPtr.Zero);
        }

        [TestMethod]
        public unsafe void can_be_cast_to_unmanaged_pointer()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            void* unmanaged = pointer;
            Assert.AreNotEqual((Int64)unmanaged, 0L);
        }

        [TestMethod]
        public void will_not_throw_on_post_increment()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            pointer = pointer++;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_assign_increment()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            pointer += 42;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_post_decrement()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            pointer = pointer--;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_throw_on_assign_decrement()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            pointer -= 42;
            Assert.IsNotNull(pointer);
        }

        [TestMethod]
        public void will_not_return_null_pointer_for_address()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            Assert.AreNotEqual(pointer.Address, UIntPtr.Zero);
        }

        [TestMethod]
        public void will_return_value_for_to_string()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            Assert.AreEqual(pointer.ToString(), value);
        }

        [TestMethod]
        public void will_return_value_for_indexer_getter()
        {
            Span<String> values = new String[] { "Foo", "Bar" };
            Pointer<String> pointer = Pointer<String>.AddressOf(ref values[0]);
            Assert.AreEqual(pointer[0], values[0]);
            Assert.AreEqual(pointer[1], values[1]);
        }

        [TestMethod]
        public void will_update_value_for_indexer_setter()
        {
            Span<String> values = new String[] { "Foo", "Bar" };
            Pointer<String> pointer = Pointer<String>.AddressOf(ref values[0]);
            pointer[0] = "420";
            Assert.AreEqual(pointer[0], "420");
        }

        [TestMethod]
        public void will_return_value_for_value_getter()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            Assert.AreEqual(pointer.Value, value);
        }

        [TestMethod]
        public void will_update_value_for_value_setter()
        {
            String value = "Foo";
            Pointer<String> pointer = Pointer<String>.AddressOf(ref value);
            pointer.Value = "420";
            Assert.AreEqual(pointer.Value, "420");
        }
    }
}