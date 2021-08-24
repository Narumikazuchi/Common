using System;
using System.Collections.ObjectModel;
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
        #region Constructor

        /// <summary>
        /// Initializes a new <see cref="Singleton"/>.
        /// </summary>
        protected Singleton()
        {
#nullable disable
            Type singleton = typeof(Singleton<>).MakeGenericType(this.GetType());
            FieldInfo creating = singleton.GetField("_creating", BindingFlags.Static | BindingFlags.NonPublic);
            Boolean bySingleton = (Boolean)creating.GetValue(null);
            if (!bySingleton)
            {
                throw new InvalidOperationException("Can't create instances of a singleton from reflection.");
            }
#nullable enable
            String? name = this.GetType().AssemblyQualifiedName;
            if (name is null)
            {
                throw new InvalidCastException("Type didn't have a qualified assembly name.");
            }
            if (_initialized.Contains(name))
            {
                throw new InvalidOperationException("Can't create multiple instances of the same singleton.");
            }
            _initialized.Add(name);
        }

        #endregion

        #region Fields

        internal static readonly Collection<String> _initialized = new();

        #endregion
    }

    /// <summary>
    /// Gives access to the instance of an <see cref="Singleton"/>.
    /// </summary>
    public static class Singleton<TClass> where TClass : Singleton
    {
        #region Constructor

        static Singleton()
        {
            if (typeof(TClass).IsAbstract)
            {
                throw new InvalidOperationException("Can't create singleton instance of an abstract class.");
            }
            ConstructorInfo[] ctors = typeof(TClass).GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (ctors.Length > 0)
            {
                throw new PublicConstructorFoundException("Public ctor() found for class " + typeof(TClass).Name + ".");
            }
        }

        #endregion

        #region Creation

        private static TClass CreateInstanceOf()
        {
            _creating = true;
            ConstructorInfo? ctor = typeof(TClass).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                                                  .FirstOrDefault(c => c.GetParameters().Length == 0);
            if (ctor is null)
            {
                throw new ConstructorNotFoundException("Non-public ctor() not found for class " + typeof(TClass).Name + ".");
            }
            TClass result = (TClass)ctor.Invoke(Array.Empty<Object>());
            _creating = false;
            return result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance for the <typeparamref name="TClass"/> class.
        /// </summary>
        public static TClass Instance => _instance.Value;

        #endregion

        #region Fields

        internal static Boolean _creating = false;
        private static readonly Lazy<TClass> _instance = new(CreateInstanceOf, LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion
    }
}
