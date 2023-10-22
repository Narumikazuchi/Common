using Narumikazuchi;

namespace Utility;

[TestClass]
public sealed class A_version
{
    [TestClass]
    public sealed class as_default_struct
    {
        [TestMethod]
        public void will_not_throw_on_creation()
        {
            AlphanumericVersion version = default;
            Assert.IsNotNull(version);
        }

        [TestMethod]
        public void will_not_throw_in_array_creation()
        {
            AlphanumericVersion[] versions = new AlphanumericVersion[3];
            Assert.IsNotNull(versions);
        }

        [TestMethod]
        public void will_throw_failed_initialization_for_major_getter()
        {
            AlphanumericVersion version = default;
            Assert.ThrowsException<FailedInitialization>(() => _ = version.Major);
        }

        [TestMethod]
        public void will_throw_failed_initialization_for_minor_getter()
        {
            AlphanumericVersion version = default;
            Assert.ThrowsException<FailedInitialization>(() => _ = version.Minor);
        }

        [TestMethod]
        public void will_throw_failed_initialization_for_build_getter()
        {
            AlphanumericVersion version = default;
            Assert.ThrowsException<FailedInitialization>(() => _ = version.Build);
        }

        [TestMethod]
        public void will_throw_failed_initialization_for_revision_getter()
        {
            AlphanumericVersion version = default;
            Assert.ThrowsException<FailedInitialization>(() => _ = version.Revision);
        }

        [TestMethod]
        public void will_create_default_struct_on_clone()
        {
            AlphanumericVersion version = default;
            AlphanumericVersion clone = version.Clone();
            Assert.AreEqual(version, clone);
        }

        [TestMethod]
        public void will_return_0_for_compareto_default()
        {
            AlphanumericVersion version = default;
            Assert.AreEqual(0, version.CompareTo(default));
        }

        [TestMethod]
        public void will_throw_failed_initialization_for_compareto_nondefault()
        {
            AlphanumericVersion version = default;
            Assert.ThrowsException<FailedInitialization>(() => version.CompareTo(new(1)));
        }

        [TestMethod]
        public void will_return_true_for_equals_default()
        {
            AlphanumericVersion version = default;
            Assert.IsTrue(version.Equals(default));
        }

        [TestMethod]
        public void will_return_false_for_equals_default()
        {
            AlphanumericVersion version = default;
            Assert.IsFalse(version.Equals(new(1)));
        }

        [TestMethod]
        public void will_throw_failed_initialization_for_tostring()
        {
            AlphanumericVersion version = default;
            Assert.ThrowsException<FailedInitialization>(version.ToString);
        }
    }

    [TestClass]
    public sealed class with_major_only_component
    {
        [TestMethod]
        public void will_not_throw_on_creation_with_valid_value()
        {
            AlphanumericVersion version = new(2);
            Assert.IsNotNull(version);
        }

        [TestMethod]
        public void will_throw_argument_null_exception_on_creation_with_null_value()
        {
            Assert.ThrowsException<NullReferenceException>(() => new AlphanumericVersion(default(String)!));
        }

        [TestMethod]
        public void will_throw_argument_exception_on_creation_with_invalid_value()
        {
            Assert.ThrowsException<ArgumentException>(() => new AlphanumericVersion("?!?"));
        }

        [TestMethod]
        public void will_return_value_for_major_getter()
        {
            AlphanumericVersion version = new(2);
            Assert.AreEqual("2", version.Major.ToString());
        }

        [TestMethod]
        public void will_return_empty_span_for_minor_getter()
        {
            AlphanumericVersion version = new(2);
            Assert.IsTrue(version.Minor.IsEmpty);
        }

        [TestMethod]
        public void will_return_empty_span_for_build_getter()
        {
            AlphanumericVersion version = new(2);
            Assert.IsTrue(version.Build.IsEmpty);
        }

        [TestMethod]
        public void will_return_empty_span_for_revision_getter()
        {
            AlphanumericVersion version = new(2);
            Assert.IsTrue(version.Revision.IsEmpty);
        }

        [TestMethod]
        public void will_return_exact_copy_on_clone()
        {
            AlphanumericVersion version = new(2);
            AlphanumericVersion clone = version.Clone();
            Assert.AreEqual(version, clone);
        }

        [TestMethod]
        public void will_return_1_for_compareto_default()
        {
            AlphanumericVersion version = new(2);
            Assert.AreEqual(1, version.CompareTo(default));
        }

        [TestMethod]
        public void will_return_0_for_compareto_equal_value()
        {
            AlphanumericVersion version = new(2);
            Assert.AreEqual(0, version.CompareTo(new(2)));
        }

        [TestMethod]
        public void will_return_negative_1_for_compareto_bigger_value()
        {
            AlphanumericVersion version = new(2);
            Assert.AreEqual(-1, version.CompareTo(new(3)));
        }

        [TestMethod]
        public void will_return_true_for_equals_equal_value()
        {
            AlphanumericVersion version = new(2);
            Assert.IsTrue(version.Equals(new(2)));
        }

        [TestMethod]
        public void will_return_false_for_equals_default()
        {
            AlphanumericVersion version = new(2);
            Assert.IsFalse(version.Equals(default));
        }

        [TestMethod]
        public void will_return_false_for_equals_nonequal_nondefault()
        {
            AlphanumericVersion version = new(2);
            Assert.IsFalse(version.Equals(new(3)));
        }

        [TestMethod]
        public void will_return_formatted_string_for_tostring()
        {
            AlphanumericVersion version = new(2);
            Assert.AreEqual("2", version.ToString());
        }
    }

    [TestClass]
    public sealed class with_major_and_minor_component
    {
        [TestMethod]
        public void will_not_throw_on_creation_with_valid_value()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.IsNotNull(version);
        }

        [TestMethod]
        public void will_throw_argument_null_exception_on_creation_with_null_value()
        {
            Assert.ThrowsException<NullReferenceException>(() => new AlphanumericVersion(default(String)!, default(String)!));
        }

        [TestMethod]
        public void will_throw_argument_exception_on_creation_with_invalid_value()
        {
            Assert.ThrowsException<ArgumentException>(() => new AlphanumericVersion(2, "?!?"));
        }

        [TestMethod]
        public void will_return_value_for_major_getter()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.AreEqual("2", version.Major.ToString());
        }

        [TestMethod]
        public void will_return_value_for_minor_getter()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.AreEqual("2", version.Minor.ToString());
        }

        [TestMethod]
        public void will_return_empty_span_for_build_getter()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.IsTrue(version.Build.IsEmpty);
        }

        [TestMethod]
        public void will_return_empty_span_for_revision_getter()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.IsTrue(version.Revision.IsEmpty);
        }

        [TestMethod]
        public void will_return_exact_copy_on_clone()
        {
            AlphanumericVersion version = new(2, 2);
            AlphanumericVersion clone = version.Clone();
            Assert.AreEqual(version, clone);
        }

        [TestMethod]
        public void will_return_1_for_compareto_default()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.AreEqual(1, version.CompareTo(default));
        }

        [TestMethod]
        public void will_return_0_for_compareto_equal_value()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.AreEqual(0, version.CompareTo(new(2, 2)));
        }

        [TestMethod]
        public void will_return_negative_1_for_compareto_bigger_value()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.AreEqual(-1, version.CompareTo(new(2, 3)));
        }

        [TestMethod]
        public void will_return_true_for_equals_equal_value()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.IsTrue(version.Equals(new(2, 2)));
        }

        [TestMethod]
        public void will_return_false_for_equals_default()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.IsFalse(version.Equals(default));
        }

        [TestMethod]
        public void will_return_false_for_equals_nonequal_nondefault()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.IsFalse(version.Equals(new(2, 3)));
        }

        [TestMethod]
        public void will_return_formatted_string_for_tostring()
        {
            AlphanumericVersion version = new(2, 2);
            Assert.AreEqual("2.2", version.ToString());
        }
    }

    [TestClass]
    public sealed class with_major_and_minor_and_build_component
    {
        [TestMethod]
        public void will_not_throw_on_creation_with_valid_value()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.IsNotNull(version);
        }

        [TestMethod]
        public void will_throw_argument_null_exception_on_creation_with_null_value()
        {
            Assert.ThrowsException<NullReferenceException>(() => new AlphanumericVersion(default(String)!, default(String)!, default(String)!));
        }

        [TestMethod]
        public void will_throw_argument_exception_on_creation_with_invalid_value()
        {
            Assert.ThrowsException<ArgumentException>(() => new AlphanumericVersion(2, 2, "?!?"));
        }

        [TestMethod]
        public void will_return_value_for_major_getter()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.AreEqual("2", version.Major.ToString());
        }

        [TestMethod]
        public void will_return_value_for_minor_getter()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.AreEqual("2", version.Minor.ToString());
        }

        [TestMethod]
        public void will_return_value_for_build_getter()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.AreEqual("2", version.Build.ToString());
        }

        [TestMethod]
        public void will_return_empty_span_for_revision_getter()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.IsTrue(version.Revision.IsEmpty);
        }

        [TestMethod]
        public void will_return_exact_copy_on_clone()
        {
            AlphanumericVersion version = new(2, 2, 2);
            AlphanumericVersion clone = version.Clone();
            Assert.AreEqual(version, clone);
        }

        [TestMethod]
        public void will_return_1_for_compareto_default()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.AreEqual(1, version.CompareTo(default));
        }

        [TestMethod]
        public void will_return_0_for_compareto_equal_value()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.AreEqual(0, version.CompareTo(new(2, 2, 2)));
        }

        [TestMethod]
        public void will_return_negative_1_for_compareto_bigger_value()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.AreEqual(-1, version.CompareTo(new(2, 2, 3)));
        }

        [TestMethod]
        public void will_return_true_for_equals_equal_value()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.IsTrue(version.Equals(new(2, 2, 2)));
        }

        [TestMethod]
        public void will_return_false_for_equals_default()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.IsFalse(version.Equals(default));
        }

        [TestMethod]
        public void will_return_false_for_equals_nonequal_nondefault()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.IsFalse(version.Equals(new(2, 2, 3)));
        }

        [TestMethod]
        public void will_return_formatted_string_for_tostring()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.AreEqual("2.2.2", version.ToString());
        }
    }

    [TestClass]
    public sealed class with_all_components
    {
        [TestMethod]
        public void will_not_throw_on_creation_with_valid_value()
        {
            AlphanumericVersion version = new(2, 2, 2);
            Assert.IsNotNull(version);
        }

        [TestMethod]
        public void will_throw_argument_null_exception_on_creation_with_null_value()
        {
            Assert.ThrowsException<NullReferenceException>(() => new AlphanumericVersion(default(String)!, default(String)!, default(String)!, default(String)!));
        }

        [TestMethod]
        public void will_throw_argument_exception_on_creation_with_invalid_value()
        {
            Assert.ThrowsException<ArgumentException>(() => new AlphanumericVersion(2, 2, 2, "?!?"));
        }

        [TestMethod]
        public void will_return_value_for_major_getter()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.AreEqual("2", version.Major.ToString());
        }

        [TestMethod]
        public void will_return_value_for_minor_getter()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.AreEqual("2", version.Minor.ToString());
        }

        [TestMethod]
        public void will_return_value_for_build_getter()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.AreEqual("2", version.Build.ToString());
        }

        [TestMethod]
        public void will_return_value_for_revision_getter()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.AreEqual("2", version.Revision.ToString());
        }

        [TestMethod]
        public void will_return_exact_copy_on_clone()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            AlphanumericVersion clone = version.Clone();
            Assert.AreEqual(version, clone);
        }

        [TestMethod]
        public void will_return_1_for_compareto_default()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.AreEqual(1, version.CompareTo(default));
        }

        [TestMethod]
        public void will_return_0_for_compareto_equal_value()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.AreEqual(0, version.CompareTo(new(2, 2, 2, 2)));
        }

        [TestMethod]
        public void will_return_negative_1_for_compareto_bigger_value()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.AreEqual(-1, version.CompareTo(new(2, 2, 2, 3)));
        }

        [TestMethod]
        public void will_return_true_for_equals_equal_value()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.IsTrue(version.Equals(new(2, 2, 2, 2)));
        }

        [TestMethod]
        public void will_return_false_for_equals_default()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.IsFalse(version.Equals(default));
        }

        [TestMethod]
        public void will_return_false_for_equals_nonequal_nondefault()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.IsFalse(version.Equals(new(2, 2, 2, 3)));
        }

        [TestMethod]
        public void will_return_formatted_string_for_tostring()
        {
            AlphanumericVersion version = new(2, 2, 2, 2);
            Assert.AreEqual("2.2.2.2", version.ToString());
        }
    }
}