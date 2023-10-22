using Narumikazuchi;

namespace Attributes;

[TestClass]
public sealed class The_attribute_resolver
{
    [TestClass]
    public sealed class will_throw_argument_null_exception
    {
        [TestMethod]
        public void if_has_attribute_assembly_parameter_is_null()
        {
            Assembly? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.HasAttribute<AssemblyOnceDummyAttribute>(value!));
        }

        [TestMethod]
        public void if_has_attribute_memberinfo_parameter_is_null()
        {
            MemberInfo? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.HasAttribute<ClassOnceDummyAttribute>(value!));
        }

        [TestMethod]
        public void if_has_attribute_parameterinfo_parameter_is_null()
        {
            ParameterInfo? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.HasAttribute<ParameterOnceDummyAttribute>(value!));
        }

        [TestMethod]
        public void if_fetch_all_attributes_assembly_parameter_is_null()
        {
            Assembly? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.FetchAllAttributes<AssemblyOnceDummyAttribute>(value!));
        }

        [TestMethod]
        public void if_fetch_all_attributes_memberinfo_parameter_is_null()
        {
            MemberInfo? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.FetchAllAttributes<ClassOnceDummyAttribute>(value!));
        }

        [TestMethod]
        public void if_fetch_all_attributes_parameterinfo_parameter_is_null()
        {
            ParameterInfo? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.FetchAllAttributes<ParameterOnceDummyAttribute>(value!));
        }

        [TestMethod]
        public void if_fetch_single_attribute_assembly_parameter_is_null()
        {
            Assembly? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.FetchSingleAttribute<AssemblyOnceDummyAttribute>(value!));
        }

        [TestMethod]
        public void if_fetch_single_attribute_memberinfo_parameter_is_null()
        {
            MemberInfo? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.FetchSingleAttribute<ClassOnceDummyAttribute>(value!));
        }

        [TestMethod]
        public void if_fetch_single_attribute_parameterinfo_parameter_is_null()
        {
            ParameterInfo? value = default;
            Assert.ThrowsException<ArgumentNullException>(() => AttributeResolver.FetchSingleAttribute<ParameterOnceDummyAttribute>(value!));
        }
    }

    [TestClass]
    public sealed class will_return_false_for_has_attribute
    {
        [TestMethod]
        public void if_assembly_does_not_have_attribute()
        {
            Assembly value = typeof(AttributeResolver).Assembly;
            Assert.AreEqual(AttributeResolver.HasAttribute<AssemblyOnceDummyAttribute>(value), false);
        }

        [TestMethod]
        public void if_memberinfo_does_not_have_attribute()
        {
            MemberInfo value = typeof(ObjectWithoutAttribute);
            Assert.AreEqual(AttributeResolver.HasAttribute<ClassOnceDummyAttribute>(value), false);
        }

        [TestMethod]
        public void if_parameterinfo_does_not_have_attribute()
        {
            ParameterInfo value = s_ParameterWithoutAttribute;
            Assert.AreEqual(AttributeResolver.HasAttribute<ParameterOnceDummyAttribute>(value), false);
        }
    }

    [TestClass]
    public sealed class will_return_true_for_has_attribute
    {
        [TestMethod]
        public void if_assembly_has_attribute()
        {
            Assembly value = typeof(The_attribute_resolver).Assembly;
            Assert.AreEqual(AttributeResolver.HasAttribute<AssemblyOnceDummyAttribute>(value), true);
        }

        [TestMethod]
        public void if_memberinfo_has_attribute()
        {
            MemberInfo value = typeof(ObjectWithAttribute);
            Assert.AreEqual(AttributeResolver.HasAttribute<ClassOnceDummyAttribute>(value), true);
        }

        [TestMethod]
        public void if_parameterinfo_has_attribute()
        {
            ParameterInfo value = s_ParameterWithAttribute;
            Assert.AreEqual(AttributeResolver.HasAttribute<ParameterOnceDummyAttribute>(value), true);
        }
    }

    [TestClass]
    public sealed class will_return_an_empty_array_for_fetch_all_attributes
    {
        [TestMethod]
        public void if_assembly_does_not_have_the_attribute()
        {
            Assembly value = typeof(AttributeResolver).Assembly;
            Assert.AreEqual(AttributeResolver.FetchAllAttributes<AssemblyMultipleDummyAttribute>(value).Length, 0);
        }

        [TestMethod]
        public void if_memberinfo_does_not_have_the_attribute()
        {
            MemberInfo value = typeof(ObjectWithoutAttribute);
            Assert.AreEqual(AttributeResolver.FetchAllAttributes<ClassMultipleDummyAttribute>(value).Length, 0);
        }

        [TestMethod]
        public void if_parameterinfo_does_not_have_the_attribute()
        {
            ParameterInfo value = s_ParameterWithoutAttribute;
            Assert.AreEqual(AttributeResolver.FetchAllAttributes<ParameterMultipleDummyAttribute>(value).Length, 0);
        }
    }

    [TestClass]
    public sealed class will_return_a_non_empty_array_for_fetch_all_attributes
    {
        [TestMethod]
        public void if_assembly_does_have_one_or_more_of_the_attribute()
        {
            Assembly value = typeof(The_attribute_resolver).Assembly;
            Assert.AreEqual(AttributeResolver.FetchAllAttributes<AssemblyMultipleDummyAttribute>(value).Length, 2);
        }

        [TestMethod]
        public void if_memberinfo_does_have_one_or_more_of_the_attribute()
        {
            MemberInfo value = typeof(ObjectWithAttribute);
            Assert.AreEqual(AttributeResolver.FetchAllAttributes<ClassMultipleDummyAttribute>(value).Length, 2);
        }

        [TestMethod]
        public void if_parameterinfo_does_have_one_or_more_of_the_attribute()
        {
            ParameterInfo value = s_ParameterWithAttribute;
            Assert.AreEqual(AttributeResolver.FetchAllAttributes<ParameterMultipleDummyAttribute>(value).Length, 2);
        }
    }

    [TestClass]
    public sealed class will_throw_a_not_allowed_exception_for_fetch_single_attribute
    {
        [TestMethod]
        public void if_assembly_does_not_have_the_attribute()
        {
            Assembly value = typeof(AttributeResolver).Assembly;
            Assert.ThrowsException<NotAllowed>(() => AttributeResolver.FetchSingleAttribute<AssemblyMultipleDummyAttribute>(value));
        }

        [TestMethod]
        public void if_assembly_does_have_the_attribute_more_than_once()
        {
            Assembly value = typeof(The_attribute_resolver).Assembly;
            Assert.ThrowsException<NotAllowed>(() => AttributeResolver.FetchSingleAttribute<AssemblyMultipleDummyAttribute>(value));
        }

        [TestMethod]
        public void if_memberinfo_does_not_have_the_attribute()
        {
            MemberInfo value = typeof(ObjectWithoutAttribute);
            Assert.ThrowsException<NotAllowed>(() => AttributeResolver.FetchSingleAttribute<ClassMultipleDummyAttribute>(value));
        }

        [TestMethod]
        public void if_memberinfo_does_have_the_attribute_more_than_once()
        {
            MemberInfo value = typeof(ObjectWithAttribute);
            Assert.ThrowsException<NotAllowed>(() => AttributeResolver.FetchSingleAttribute<ClassMultipleDummyAttribute>(value));
        }

        [TestMethod]
        public void if_parameterinfo_does_not_have_the_attribute()
        {
            ParameterInfo value = s_ParameterWithoutAttribute;
            Assert.ThrowsException<NotAllowed>(() => AttributeResolver.FetchSingleAttribute<ParameterMultipleDummyAttribute>(value));
        }

        [TestMethod]
        public void if_parameterinfo_does_have_the_attribute_more_than_once()
        {
            ParameterInfo value = s_ParameterWithAttribute;
            Assert.ThrowsException<NotAllowed>(() => AttributeResolver.FetchSingleAttribute<ParameterMultipleDummyAttribute>(value));
        }
    }

    [TestClass]
    public sealed class will_return_the_attribute_instance_for_fetch_single_attribute
    {
        [TestMethod]
        public void if_assembly_does_have_the_attribute()
        {
            Assembly value = typeof(The_attribute_resolver).Assembly;
            Assert.IsNotNull(AttributeResolver.FetchSingleAttribute<AssemblyOnceDummyAttribute>(value));
        }

        [TestMethod]
        public void if_memberinfo_does_have_the_attribute()
        {
            MemberInfo value = typeof(ObjectWithAttribute);
            Assert.IsNotNull(AttributeResolver.FetchSingleAttribute<ClassOnceDummyAttribute>(value));
        }

        [TestMethod]
        public void if_parameterinfo_does_have_the_attribute()
        {
            ParameterInfo value = s_ParameterWithAttribute;
            Assert.IsNotNull(AttributeResolver.FetchSingleAttribute<ParameterOnceDummyAttribute>(value));
        }
    }

    static The_attribute_resolver()
    {
        Type type = typeof(ObjectWithoutAttribute);
        MethodInfo method = type.GetMethod(nameof(ObjectWithoutAttribute.MethodWithoutAttributedParameter))!;
        s_ParameterWithoutAttribute = method.GetParameters()[0];

        type = typeof(ObjectWithAttribute);
        method = type.GetMethod(nameof(ObjectWithAttribute.MethodWithAttributedParameter))!;
        s_ParameterWithAttribute = method.GetParameters()[0];
    }

    static private readonly ParameterInfo s_ParameterWithoutAttribute;
    static private readonly ParameterInfo s_ParameterWithAttribute;
}