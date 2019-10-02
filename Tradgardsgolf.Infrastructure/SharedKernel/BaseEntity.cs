using System;
using System.Reflection;

namespace Tradgardsgolf.Infrastructure.SharedKernel
{
    public class BaseEntity<T> where T : class
    {
        protected T SetOptions<TBuilder>(Action<TBuilder> options) where TBuilder : BaseEntityBuilder<T>
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var args = new[] { this };

            var builder = (TBuilder)Activator.CreateInstance(typeof(TBuilder), flags, null, new[] { this }, null);
            options?.Invoke(builder);

            return builder.Build();
        }
    }
}
