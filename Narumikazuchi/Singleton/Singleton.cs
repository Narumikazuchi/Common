using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Narumikazuchi
{
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
            FieldInfo creating = singleton.GetField("_creating", 
                                                    BindingFlags.Static | BindingFlags.NonPublic);
            Boolean bySingleton = (Boolean)creating.GetValue(null);
            if (!bySingleton)
            {
                throw new InvalidOperationException(REFLECTION_IS_INVALID);
            }
            String name = this.GetType()
                              .AssemblyQualifiedName;
            if (_initialized.Contains(name))
            {
                throw new InvalidOperationException(MULTIPLE_INSTANCES_ARE_INVALID);
            }
            _initialized.Add(name);
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
                throw new InvalidOperationException(CANT_CREATE_ABSTRACT_CLASSES);
            }
            ConstructorInfo[] ctors = typeof(TClass).GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (ctors.Length > 0)
            {
                throw new PublicConstructorFoundException(String.Format(PUBLIC_CONSTRUCTORS_NOT_ALLOWED, 
                                                                        typeof(TClass).Name));
            }
        }

        private static TClass CreateInstanceOf()
        {
            _creating = true;
            ConstructorInfo? ctor = typeof(TClass).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                                                  .FirstOrDefault(c => c.GetParameters().Length == 0);
            if (ctor is null)
            {
                throw new ConstructorNotFoundException(String.Format(NO_NONPUBLIC_CONSTRUCTORS_FOUND, 
                                                                     typeof(TClass).Name));
            }
            TClass result = (TClass)ctor.Invoke(Array.Empty<Object>());
            _creating = false;
            return result;
        }

        /// <summary>
        /// Gets the singleton instance for the <typeparamref name="TClass"/> class.
        /// </summary>
        [Pure]
        public static TClass Instance => _instance.Value;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal static Boolean _creating = false;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Lazy<TClass> _instance = new(CreateInstanceOf, 
                                                             LazyThreadSafetyMode.ExecutionAndPublication);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const String CANT_CREATE_ABSTRACT_CLASSES = "Can't create singleton instance of an abstract class.";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const String PUBLIC_CONSTRUCTORS_NOT_ALLOWED = "Public constructors are not allowed for singletons but found one for class {0}.";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const String NO_NONPUBLIC_CONSTRUCTORS_FOUND = "No non-public constructor found for class {0} to instantiate it as singleton.";
    }
}
