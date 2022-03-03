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
        Type singleton = typeof(Singleton<>).MakeGenericType(this.GetType());
        FieldInfo creating = singleton.GetField(name: "s_Creating", 
                                                bindingAttr: BindingFlags.Static | BindingFlags.NonPublic)!;
        Boolean bySingleton = (Boolean)creating.GetValue(null)!;
        if (!bySingleton)
        {
            throw new NotAllowed(message: REFLECTION_IS_INVALID,
                                 ("Typename", this.GetType()
                                                  .FullName),
                                 ("_creating", bySingleton));
        }
        String name = this.GetType()
                          .AssemblyQualifiedName!;
        if (s_Initialized.Contains(item: name))
        {
            throw new NotAllowed(message: MULTIPLE_INSTANCES_ARE_INVALID,
                                 ("Typename", this.GetType()
                                                  .FullName),
                                 ("_creating", bySingleton),
                                 ("AssemblyQualifiedName", this.GetType()
                                                               .AssemblyQualifiedName));
        }
        s_Initialized.Add(name);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal static readonly Collection<String> s_Initialized = new();

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
            throw new NotAllowed(message: CANT_CREATE_ABSTRACT_CLASSES,
                                 ("Typename", typeof(TClass).FullName));
        }
        ConstructorInfo[] ctors = typeof(TClass).GetConstructors(bindingAttr: BindingFlags.Instance | BindingFlags.Public);
        if (ctors.Length > 0)
        {
            throw new PublicConstructorFound(message: String.Format(format: PUBLIC_CONSTRUCTORS_NOT_ALLOWED, 
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
    [Pure]
    public static TClass Instance => 
        s_Instance.Value;

    private static TClass CreateInstanceOf()
    {
        s_Creating = true;
        ConstructorInfo? ctor = typeof(TClass).GetConstructors(bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic)
                                              .FirstOrDefault(c => c.GetParameters()
                                                                    .Length == 0);
        if (ctor is null)
        {
            throw new ConstructorNotFound(message: String.Format(format: NO_NONPUBLIC_CONSTRUCTORS_FOUND, 
                                                                 arg0: typeof(TClass).Name),
                                          ("Typename", typeof(TClass).FullName));
        }
        TClass result = (TClass)ctor.Invoke(parameters: Array.Empty<Object>());
        s_Creating = false;
        return result;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal static Boolean s_Creating = false;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly Lazy<TClass> s_Instance = new(valueFactory: CreateInstanceOf, 
                                                          mode: LazyThreadSafetyMode.ExecutionAndPublication);
    
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String CANT_CREATE_ABSTRACT_CLASSES = "Can't create singleton instance of an abstract class.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String PUBLIC_CONSTRUCTORS_NOT_ALLOWED = "Public constructors are not allowed for singletons but found one for class {0}.";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const String NO_NONPUBLIC_CONSTRUCTORS_FOUND = "No non-public, parameterless constructor found for class {0} to instantiate it as singleton.";
}