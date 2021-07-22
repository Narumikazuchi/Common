using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Narumikazuchi
{
    /// <summary>
    /// Provides derived classes with a way to force the singleton pattern.
    /// </summary>
    public interface ISingleton<T> where T : class
    {
        #region Constructor

        static ISingleton()
        {
            ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (ctors.Length > 0)
            {
                throw new PublicConstructorFoundException("Public ctor() found for class " + typeof(T).Name + ".");
            }
        }

        #endregion

        #region Creation

        private static T CreateInstanceOf()
        {
            ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            ConstructorInfo? ctor = ctors.FirstOrDefault(c => c.GetParameters().Length == 0);
            return ctor is null ? throw new ConstructorNotFoundException("Non-public ctor() not found for class " + typeof(T).Name + ".") : (T)ctor.Invoke(Array.Empty<Object>());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance for the <typeparamref name="T"/> class.
        /// </summary>
        public static T Instance => _instance.Value;

        #endregion

        #region Fields

        private static readonly Lazy<T> _instance = new(CreateInstanceOf, LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion
    }
}
