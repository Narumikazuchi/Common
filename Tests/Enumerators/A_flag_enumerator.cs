using Narumikazuchi;
using Narumikazuchi.Collections;

namespace Enumerators;

[TestClass]
public sealed class A_flag_enumerator
{
    [TestMethod]
    public void does_not_enumerate_when__value_is_default_struct()
    {
        FlagEnumerator<ConsoleColor> enumerator = default;
        Assert.IsFalse(enumerator.MoveNext());
    }

    [TestMethod]
    public void throws_a_not_allowed_exception_for_non_flag_enum_parameter()
    {
        Assert.ThrowsException<NotAllowed>(() => new FlagEnumerator<ConsoleColor>(ConsoleColor.Yellow));
    }

    [TestMethod]
    [DynamicData(nameof(TestValuesAttributeTargets))]
    public void enumerates_all_set_flags_of_example_AttributeTargets(AttributeTargets values, HashSet<AttributeTargets> control)
    {
        this.enumerates_all_set_flags_Helper(values, control);
    }

    [TestMethod]
    [DynamicData(nameof(TestValuesAssemblyFlags))]
    public void enumerates_all_set_flags_of_example_AssemblyFlags(AssemblyFlags values, HashSet<AssemblyFlags> control)
    {
        this.enumerates_all_set_flags_Helper(values, control);
    }

    private void enumerates_all_set_flags_Helper<TEnum>(TEnum @enum, HashSet<TEnum> control)
        where TEnum : struct, Enum
    {
        FlagEnumerator<TEnum> enumerator = new(@enum);
        HashSet<TEnum> values = new(enumerator);
        Assert.IsTrue(values.SetEquals(control));
    }

    static public IEnumerable<Object[]> TestValuesAttributeTargets
    {
        get
        {
            return new Object[][]
            {
                new Object[]
                {
                    AttributeTargets.All,
                    new HashSet<AttributeTargets>()
                    {
                        AttributeTargets.Assembly,
                        AttributeTargets.Module,
                        AttributeTargets.Class,
                        AttributeTargets.Struct,
                        AttributeTargets.Enum,
                        AttributeTargets.Constructor,
                        AttributeTargets.Method,
                        AttributeTargets.Property,
                        AttributeTargets.Field,
                        AttributeTargets.Event,
                        AttributeTargets.Interface,
                        AttributeTargets.Parameter,
                        AttributeTargets.Delegate,
                        AttributeTargets.ReturnValue,
                        AttributeTargets.GenericParameter,
                        AttributeTargets.All
                    }
                },
                new Object[]
                {
                    AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface,
                    new HashSet<AttributeTargets>()
                    {
                        AttributeTargets.Class,
                        AttributeTargets.Struct,
                        AttributeTargets.Enum,
                        AttributeTargets.Interface
                    }
                }
            };
        }
    }

    static public IEnumerable<Object[]> TestValuesAssemblyFlags
    {
        get
        {
            return new Object[][]
            {
                new Object[]
                {
                    AssemblyFlags.PublicKey | AssemblyFlags.Retargetable | AssemblyFlags.EnableJitCompileTracking,
                    new HashSet<AssemblyFlags>()
                    {
                        AssemblyFlags.PublicKey,
                        AssemblyFlags.Retargetable,
                        AssemblyFlags.EnableJitCompileTracking
                    }
                },
                new Object[]
                {
                    AssemblyFlags.WindowsRuntime | AssemblyFlags.Retargetable | AssemblyFlags.EnableJitCompileTracking,
                    new HashSet<AssemblyFlags>()
                    {
                        AssemblyFlags.WindowsRuntime,
                        AssemblyFlags.Retargetable,
                        AssemblyFlags.EnableJitCompileTracking
                    }
                }
            };
        }
    }
}