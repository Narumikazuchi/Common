namespace Narumikazuchi;

/// <summary>
/// Provides derived classes with a way to force the singleton pattern.
/// </summary>
public abstract class Singleton
{
    /// <summary>
    /// Initializes a new <see cref="Singleton"/>.
    /// </summary>
    protected Singleton()
    {
#nullable disable
        Type singleton = typeof(Singleton<>).MakeGenericType(this.GetType());
        FieldInfo creating = singleton.GetField(name: "_creating", 
                                                bindingAttr: BindingFlags.Static | BindingFlags.NonPublic);
        Boolean bySingleton = (Boolean)creating.GetValue(obj: null);
        if (!bySingleton)
        {
            throw new NotAllowed(auxMessage: REFLECTION_IS_INVALID,
                                 ("Typename", this.GetType().FullName),
                                 ("_creating", bySingleton));
        }
        String name = this.GetType()
                          .AssemblyQualifiedName;
        if (_initialized.Contains(item: name))
        {
            throw new NotAllowed(auxMessage: MULTIPLE_INSTANCES_ARE_INVALID,
                                 ("Typename", this.GetType().FullName),
                                 ("_creating", bySingleton),
                                 ("AssemblyQualifiedName", this.GetType().AssemblyQualifiedName));
        }
        _initialized.Add(item: name);
#nullable enable
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal static readonly Collection<String> _initialized = new();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String REFLECTION_IS_INVALID = "Can't create instances of a singleton from reflection.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String MULTIPLE_INSTANCES_ARE_INVALID = "Can't create multiple instances of the same singleton.";
}

/// <summary>
/// Gives access to the instance of an <see cref="Singleton"/>.
/// </summary>
public static class Singleton<TClass> 
    where TClass : Singleton
{
    static Singleton()
    {
        if (typeof(TClass).IsAbstract)
        {
            throw new NotAllowed(auxMessage: CANT_CREATE_ABSTRACT_CLASSES,
                                 ("Typename", typeof(TClass).FullName));
        }
        ConstructorInfo[] ctors = typeof(TClass).GetConstructors(bindingAttr: BindingFlags.Instance | BindingFlags.Public);
        if (ctors.Length > 0)
        {
            throw new PublicConstructorFound(auxMessage: String.Format(format: PUBLIC_CONSTRUCTORS_NOT_ALLOWED, 
                                                                       arg0: typeof(TClass).Name),
                                             ("Typename", typeof(TClass).FullName));
        }
    }

    /// <summary>
    /// Gets the singleton instance for the <typeparamref name="TClass"/> class.
    /// </summary>
    /// <exception cref="ConstructorNotFound"/>
    /// <exception cref="NotAllowed"/>
    /// <exception cref="PublicConstructorFound"/>
    [DisallowNull]
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
    [Pure]
    public static TClass Instance => _instance.Value;

    private static TClass CreateInstanceOf()
    {
        _creating = true;
        ConstructorInfo? ctor = typeof(TClass).GetConstructors(bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)
                                              .FirstOrDefault(c => c.GetParameters().Length == 0);
        if (ctor is null)
        {
            throw new ConstructorNotFound(auxMessage: String.Format(format: NO_NONPUBLIC_CONSTRUCTORS_FOUND, 
                                                                    arg0: typeof(TClass).Name),
                                          ("Typename", typeof(TClass).FullName));
        }
        TClass result = (TClass)ctor.Invoke(parameters: Array.Empty<Object>());
        _creating = false;
        return result;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal static Boolean _creating = false;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly Lazy<TClass> _instance = new(valueFactory: CreateInstanceOf, 
                                                         mode: LazyThreadSafetyMode.ExecutionAndPublication);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String CANT_CREATE_ABSTRACT_CLASSES = "Can't create singleton instance of an abstract class.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String PUBLIC_CONSTRUCTORS_NOT_ALLOWED = "Public constructors are not allowed for singletons but found one for class {0}.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String NO_NONPUBLIC_CONSTRUCTORS_FOUND = "No non-public, parameterless constructor found for class {0} to instantiate it as singleton.";
}